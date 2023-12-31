using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.State.Entities;
using KitTraden.AnAbundanceOfMana.State.State;

namespace KitTraden.AnAbundanceOfMana.Calculators.CardLocation
{
    public class CardLocationCalculator
    {
        public static List<LocationOfCardThatHasMoved> GetLocationsOfCardsThatMoved(CardsState previousState, CardsState currentState)
        {
            var locationsOfCardsThatHaveMoved = new List<LocationOfCardThatHasMoved>();

            // We use HashSets and precompute each card's location in its pile so that this function can
            // run in O(n) with respect to the number of cards. Using only lists would be easier to read,
            // but this would be O(n^2) wrt # of cards because we'd have to search through the lists once
            // per card to determine where it is. With potentially hundreds of cards in play and this function
            // being called many times each time a card is played, I'm worried O(n^2) would be poor enough
            // to cause noticable lag on slower devices.
            var previousStateLocations = new List<(CardLocation.LocationType, HashSet<Card>, Dictionary<Card, int>)>
            {
                (CardLocation.LocationType.COMBO, new HashSet<Card>(previousState.Combo, UUIDCardEqualityComparer.INSTANCE), CalculateCardPositions(previousState.Combo)),
                (CardLocation.LocationType.DECK, new HashSet<Card>(previousState.Deck, UUIDCardEqualityComparer.INSTANCE), CalculateCardPositions(previousState.Deck)),
                (CardLocation.LocationType.DISCARD_PILE, new HashSet<Card>(previousState.DiscardPile, UUIDCardEqualityComparer.INSTANCE), CalculateCardPositions(previousState.DiscardPile)),
                (CardLocation.LocationType.EXHAUST_PILE, new HashSet<Card>(previousState.ExhaustPile, UUIDCardEqualityComparer.INSTANCE), CalculateCardPositions(previousState.ExhaustPile)),
                (CardLocation.LocationType.HAND, new HashSet<Card>(previousState.Hand, UUIDCardEqualityComparer.INSTANCE), CalculateCardPositions(previousState.Hand)),
                (CardLocation.LocationType.SPENT_POWERS_PILE, new HashSet<Card>(previousState.SpentPowerPile, UUIDCardEqualityComparer.INSTANCE), CalculateCardPositions(previousState.SpentPowerPile))
            };

            var allCardsInPreviousState = new HashSet<Card>(UUIDCardEqualityComparer.INSTANCE);
            allCardsInPreviousState.UnionWith(previousState.Combo);
            allCardsInPreviousState.UnionWith(previousState.Deck);
            allCardsInPreviousState.UnionWith(previousState.DiscardPile);
            allCardsInPreviousState.UnionWith(previousState.ExhaustPile);
            allCardsInPreviousState.UnionWith(previousState.Hand);
            allCardsInPreviousState.UnionWith(previousState.SpentPowerPile);

            var currentStateLocations = new List<(CardLocation.LocationType, HashSet<Card>, Dictionary<Card, int>)>
            {
                (CardLocation.LocationType.COMBO, new HashSet<Card>(currentState.Combo, UUIDCardEqualityComparer.INSTANCE), CalculateCardPositions(currentState.Combo)),
                (CardLocation.LocationType.DECK, new HashSet<Card>(currentState.Deck, UUIDCardEqualityComparer.INSTANCE), CalculateCardPositions(currentState.Deck)),
                (CardLocation.LocationType.DISCARD_PILE, new HashSet<Card>(currentState.DiscardPile, UUIDCardEqualityComparer.INSTANCE), CalculateCardPositions(currentState.DiscardPile)),
                (CardLocation.LocationType.EXHAUST_PILE, new HashSet<Card>(currentState.ExhaustPile, UUIDCardEqualityComparer.INSTANCE), CalculateCardPositions(currentState.ExhaustPile)),
                (CardLocation.LocationType.HAND, new HashSet<Card>(currentState.Hand, UUIDCardEqualityComparer.INSTANCE), CalculateCardPositions(currentState.Hand)),
                (CardLocation.LocationType.SPENT_POWERS_PILE, new HashSet<Card>(currentState.SpentPowerPile, UUIDCardEqualityComparer.INSTANCE), CalculateCardPositions(currentState.SpentPowerPile))
            };

            var allCardsInCurrentState = new HashSet<Card>(UUIDCardEqualityComparer.INSTANCE);
            allCardsInCurrentState.UnionWith(currentState.Combo);
            allCardsInCurrentState.UnionWith(currentState.Deck);
            allCardsInCurrentState.UnionWith(currentState.DiscardPile);
            allCardsInCurrentState.UnionWith(currentState.ExhaustPile);
            allCardsInCurrentState.UnionWith(currentState.Hand);
            allCardsInCurrentState.UnionWith(currentState.SpentPowerPile);

            // This gets all of the cards that existed in the previous state and have either
            // been removed from the current state or moved to a new location in the current state
            foreach (var (previousLocationType, previousCards, previousCardLocations) in previousStateLocations)
            {
                foreach (var previousCard in previousCards)
                {
                    var previousCardLocation = previousCardLocations[previousCard];

                    if (allCardsInCurrentState.Contains(previousCard))
                    {
                        foreach (var (currentLocationType, currentCards, currentCardLocations) in currentStateLocations)
                        {
                            if (currentCards.Contains(previousCard))
                            {
                                currentCards.TryGetValue(previousCard, out Card currentCard);
                                var currentCardLocation = currentCardLocations[currentCard];

                                if (currentCardLocation != previousCardLocation || currentLocationType != previousLocationType)
                                {
                                    locationsOfCardsThatHaveMoved.Add(new LocationOfCardThatHasMoved
                                    {
                                        cardInPreviousState = previousCard,
                                        cardInCurrentState = currentCard,
                                        locationInPreviousState = new CardLocation { index = previousCardLocation, location = previousLocationType },
                                        locationInCurrentState = new CardLocation { index = currentCardLocation, location = currentLocationType }
                                    });
                                }

                                break;
                            }
                        }
                    }
                    else
                    {
                        locationsOfCardsThatHaveMoved.Add(new LocationOfCardThatHasMoved
                        {
                            cardInPreviousState = previousCard,
                            cardInCurrentState = null,
                            locationInPreviousState = new CardLocation { index = previousCardLocation, location = previousLocationType },
                            locationInCurrentState = new CardLocation { index = -1, location = CardLocation.LocationType.NONE }
                        });
                    }
                }
            }

            // This gets all of the cards that weren't in the previous state but are in the current state
            foreach (var (currentLocationType, currentCards, currentCardLocations) in currentStateLocations)
            {
                foreach (var currentCard in currentCards)
                {
                    if (!allCardsInPreviousState.Contains(currentCard))
                    {
                        var currentCardLocation = currentCardLocations[currentCard];

                        locationsOfCardsThatHaveMoved.Add(new LocationOfCardThatHasMoved
                        {
                            cardInPreviousState = null,
                            cardInCurrentState = currentCard,
                            locationInPreviousState = new CardLocation { index = -1, location = CardLocation.LocationType.NONE },
                            locationInCurrentState = new CardLocation { index = currentCardLocation, location = currentLocationType }
                        });
                    }
                }
            }

            return locationsOfCardsThatHaveMoved;
        }

        private static Dictionary<Card, int> CalculateCardPositions(List<Card> cards)
        {
            var result = new Dictionary<Card, int>();

            for (int i = 0; i < cards.Count; i++)
            {
                result.Add(cards[i], i);
            }

            return result;
        }

        public static CardLocation GetCardLocation(Card thisCard, CardsState cardsState)
        {
            // No need to put cards into hashmaps because we only need to search through
            // the cards once. This is O(n) with respect to # of cards either way.
            var locations = new (CardLocation.LocationType, List<Card>)[]
            {
                (CardLocation.LocationType.COMBO, cardsState.Combo),
                (CardLocation.LocationType.DECK, cardsState.Deck),
                (CardLocation.LocationType.DISCARD_PILE, cardsState.DiscardPile),
                (CardLocation.LocationType.EXHAUST_PILE, cardsState.ExhaustPile),
                (CardLocation.LocationType.HAND, cardsState.Hand),
                (CardLocation.LocationType.SPENT_POWERS_PILE, cardsState.SpentPowerPile)
            };

            foreach (var (location, cardPile) in locations)
            {
                int index = cardPile.FindIndex((card) => card.Equals(thisCard));
                if (index != -1)
                {
                    return new CardLocation()
                    {
                        location = location,
                        index = index
                    };
                }
            }

            return new CardLocation()
            {
                location = CardLocation.LocationType.NONE,
                index = -1
            };
        }
    }
}