using System;
using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.Calculators.CardLocation;
using KitTraden.AnAbundanceOfMana.State.Entities;
using KitTraden.AnAbundanceOfMana.State.State;
using KitTraden.AnAbundanceOfMana.Tests.Factories.State.Entities;
using NUnit.Framework;

public class CardLocationCalculatorTest
{
    [Test]
    public void GetLocationsOfCardsThatMoved_NoCardsInEitherState_ReturnsEmptyList()
    {
        var emptyDeck = new List<Card>();
        var previousState = new CardsState(emptyDeck);
        var currentState = new CardsState(emptyDeck);

        var result = CardLocationCalculator.GetLocationsOfCardsThatMoved(previousState, currentState);

        Assert.That(result, Has.Count.EqualTo(0));
    }

    [Test, Combinatorial]
    public void GetLocationsOfCardsThatMoved_WhenCardMoves_ReturnsCorrectList(
        [ValueSource(typeof(CardLocation), "AllPileLocations")] CardLocation.LocationType locationToMoveFrom,
        [ValueSource(typeof(CardLocation), "AllPileLocations")] CardLocation.LocationType locationToMoveTo
    )
    {
        var cardToMove = CardFactory.CreateCard();
        var currentCardToMove = CardFactory.CopyCard(cardToMove);

        var sameCombo = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameDeck = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameDiscardPile = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameExhaustPile = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameHand = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameSpentPowersPile = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };

        var previousState = new CardsState(CardFactory.CopyCards(sameDeck))
        {
            Combo = sameCombo,
            Deck = sameDeck,
            DiscardPile = sameDiscardPile,
            ExhaustPile = sameExhaustPile,
            Hand = sameHand,
            SpentPowerPile = sameSpentPowersPile
        };

        var currentState = new CardsState(CardFactory.CopyCards(sameDeck))
        {
            Combo = CardFactory.CopyCards(sameCombo),
            Deck = CardFactory.CopyCards(sameDeck),
            DiscardPile = CardFactory.CopyCards(sameDiscardPile),
            ExhaustPile = CardFactory.CopyCards(sameExhaustPile),
            Hand = CardFactory.CopyCards(sameHand),
            SpentPowerPile = CardFactory.CopyCards(sameSpentPowersPile)
        };

        var pileToMoveFrom = locationToMoveFrom switch
        {
            CardLocation.LocationType.COMBO => previousState.Combo,
            CardLocation.LocationType.DISCARD_PILE => previousState.DiscardPile,
            CardLocation.LocationType.DECK => previousState.Deck,
            CardLocation.LocationType.EXHAUST_PILE => previousState.ExhaustPile,
            CardLocation.LocationType.HAND => previousState.Hand,
            CardLocation.LocationType.SPENT_POWERS_PILE => previousState.SpentPowerPile,
            _ => throw new Exception($"Unknown card location type {locationToMoveFrom}")
        };
        var pileToMoveTo = locationToMoveTo switch
        {
            CardLocation.LocationType.COMBO => currentState.Combo,
            CardLocation.LocationType.DISCARD_PILE => currentState.DiscardPile,
            CardLocation.LocationType.DECK => currentState.Deck,
            CardLocation.LocationType.EXHAUST_PILE => currentState.ExhaustPile,
            CardLocation.LocationType.HAND => currentState.Hand,
            CardLocation.LocationType.SPENT_POWERS_PILE => currentState.SpentPowerPile,
            _ => throw new Exception($"Unknown card location type {locationToMoveTo}")
        };

        pileToMoveFrom.Add(cardToMove);
        pileToMoveTo.Insert(0, currentCardToMove);

        var expectedResult = new List<LocationOfCardThatHasMoved>()
        {
            new LocationOfCardThatHasMoved()
            {
                cardInPreviousState = cardToMove,
                cardInCurrentState = currentCardToMove,
                locationInPreviousState = new CardLocation() { location = locationToMoveFrom, index = 2 },
                locationInCurrentState = new CardLocation() { location = locationToMoveTo, index = 0 }
            }
        };

        var previousPileToMoveTo = locationToMoveTo switch
        {
            CardLocation.LocationType.COMBO => previousState.Combo,
            CardLocation.LocationType.DISCARD_PILE => previousState.DiscardPile,
            CardLocation.LocationType.DECK => previousState.Deck,
            CardLocation.LocationType.EXHAUST_PILE => previousState.ExhaustPile,
            CardLocation.LocationType.HAND => previousState.Hand,
            CardLocation.LocationType.SPENT_POWERS_PILE => previousState.SpentPowerPile,
            _ => throw new Exception($"Unknown card location type {locationToMoveTo}")
        };

        for (int index = 1; // we already accounted for the first card in the element we created above
             index < pileToMoveTo.Count; index++)
        {
            expectedResult.Add(new LocationOfCardThatHasMoved()
            {
                cardInPreviousState = previousPileToMoveTo[index - 1],
                cardInCurrentState = pileToMoveTo[index],
                locationInPreviousState = new CardLocation() { location = locationToMoveTo, index = index - 1 },
                locationInCurrentState = new CardLocation() { location = locationToMoveTo, index = index }
            });
        }

        var result = CardLocationCalculator.GetLocationsOfCardsThatMoved(previousState, currentState);

        Assert.That(result, Is.EquivalentTo(expectedResult));
    }

    [Test]
    public void GetLocationsOfCardsThatMoved_WhenCardIsCreated_ReturnsCorrectList(
        [ValueSource(typeof(CardLocation), "AllPileLocations")] CardLocation.LocationType locationInWhichToCreate
    )
    {
        var cardToCreate = CardFactory.CreateCard();

        var sameCombo = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameDeck = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameDiscardPile = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameExhaustPile = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameHand = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameSpentPowersPile = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };

        var previousState = new CardsState(CardFactory.CopyCards(sameDeck))
        {
            Combo = sameCombo,
            Deck = sameDeck,
            DiscardPile = sameDiscardPile,
            ExhaustPile = sameExhaustPile,
            Hand = sameHand,
            SpentPowerPile = sameSpentPowersPile
        };

        var currentState = new CardsState(CardFactory.CopyCards(sameDeck))
        {
            Combo = CardFactory.CopyCards(sameCombo),
            Deck = CardFactory.CopyCards(sameDeck),
            DiscardPile = CardFactory.CopyCards(sameDiscardPile),
            ExhaustPile = CardFactory.CopyCards(sameExhaustPile),
            Hand = CardFactory.CopyCards(sameHand),
            SpentPowerPile = CardFactory.CopyCards(sameSpentPowersPile)
        };

        var pileInWhichToCreate = locationInWhichToCreate switch
        {
            CardLocation.LocationType.COMBO => currentState.Combo,
            CardLocation.LocationType.DISCARD_PILE => currentState.DiscardPile,
            CardLocation.LocationType.DECK => currentState.Deck,
            CardLocation.LocationType.EXHAUST_PILE => currentState.ExhaustPile,
            CardLocation.LocationType.HAND => currentState.Hand,
            CardLocation.LocationType.SPENT_POWERS_PILE => currentState.SpentPowerPile,
            _ => throw new Exception($"Unknown card location type {locationInWhichToCreate}")
        };

        pileInWhichToCreate.Add(cardToCreate);

        var expectedResult = new List<LocationOfCardThatHasMoved>()
        {
            new LocationOfCardThatHasMoved()
            {
                cardInPreviousState = null,
                cardInCurrentState = cardToCreate,
                locationInPreviousState = new CardLocation() { location = CardLocation.LocationType.NONE, index = -1 },
                locationInCurrentState = new CardLocation() { location = locationInWhichToCreate, index = pileInWhichToCreate.Count - 1 }
            }
        };

        var result = CardLocationCalculator.GetLocationsOfCardsThatMoved(previousState, currentState);

        Assert.That(result, Is.EquivalentTo(expectedResult));
    }

    [Test]
    public void GetLocationsOfCardsThatMoved_WhenCardIsDestroyed_ReturnsCorrectList(
        [ValueSource(typeof(CardLocation), "AllPileLocations")] CardLocation.LocationType locationFromWhichToDestroy
    )
    {
        var cardToDestroy = CardFactory.CreateCard();

        var sameCombo = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameDeck = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameDiscardPile = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameExhaustPile = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameHand = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };
        var sameSpentPowersPile = new List<Card>
        {
            CardFactory.CreateCard(),
            CardFactory.CreateCard()
        };

        var previousState = new CardsState(CardFactory.CopyCards(sameDeck))
        {
            Combo = sameCombo,
            Deck = sameDeck,
            DiscardPile = sameDiscardPile,
            ExhaustPile = sameExhaustPile,
            Hand = sameHand,
            SpentPowerPile = sameSpentPowersPile
        };

        var currentState = new CardsState(CardFactory.CopyCards(sameDeck))
        {
            Combo = CardFactory.CopyCards(sameCombo),
            Deck = CardFactory.CopyCards(sameDeck),
            DiscardPile = CardFactory.CopyCards(sameDiscardPile),
            ExhaustPile = CardFactory.CopyCards(sameExhaustPile),
            Hand = CardFactory.CopyCards(sameHand),
            SpentPowerPile = CardFactory.CopyCards(sameSpentPowersPile)
        };

        var pileFromWhichToDestroy = locationFromWhichToDestroy switch
        {
            CardLocation.LocationType.COMBO => previousState.Combo,
            CardLocation.LocationType.DISCARD_PILE => previousState.DiscardPile,
            CardLocation.LocationType.DECK => previousState.Deck,
            CardLocation.LocationType.EXHAUST_PILE => previousState.ExhaustPile,
            CardLocation.LocationType.HAND => previousState.Hand,
            CardLocation.LocationType.SPENT_POWERS_PILE => previousState.SpentPowerPile,
            _ => throw new Exception($"Unknown card location type {locationFromWhichToDestroy}")
        };

        pileFromWhichToDestroy.Add(cardToDestroy);

        var expectedResult = new List<LocationOfCardThatHasMoved>()
        {
            new LocationOfCardThatHasMoved()
            {
                cardInPreviousState = cardToDestroy,
                cardInCurrentState = null,
                locationInPreviousState = new CardLocation() { location = locationFromWhichToDestroy, index = pileFromWhichToDestroy.Count - 1 },
                locationInCurrentState = new CardLocation() { location = CardLocation.LocationType.NONE, index = -1 }
            }
        };

        var result = CardLocationCalculator.GetLocationsOfCardsThatMoved(previousState, currentState);

        Assert.That(result, Is.EquivalentTo(expectedResult));
    }
}
