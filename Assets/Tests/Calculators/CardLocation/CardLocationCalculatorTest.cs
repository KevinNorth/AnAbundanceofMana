using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.Calculators.CardLocation;
using KitTraden.AnAbundanceOfMana.State.Entities;
using KitTraden.AnAbundanceOfMana.State.State;
using KitTraden.AnAbundanceOfMana.Tests.Fixtures.State.Entities;
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

    [Test]
    public void GetLocationsOfCardsThatMoved_CardMovedFromHandToCombo_ReturnsCorrectList()
    {
        var cardToMove = CardFixtures.CreateCard();

        var sameDeck = new List<Card>
        {
            CardFixtures.CreateCard(),
            CardFixtures.CreateCard()
        };
        var sameHand = new List<Card>
        {
            CardFixtures.CreateCard(),
            CardFixtures.CreateCard()
        };
        var sameCombo = new List<Card>
        {
            CardFixtures.CreateCard(),
            CardFixtures.CreateCard()
        };
        var sameDiscardPile = new List<Card>
        {
            CardFixtures.CreateCard(),
            CardFixtures.CreateCard()
        };
        var sameExhaustPile = new List<Card>
        {
            CardFixtures.CreateCard(),
            CardFixtures.CreateCard()
        };
        var sameSpentPowersPile = new List<Card>
        {
            CardFixtures.CreateCard(),
            CardFixtures.CreateCard()
        };

        var previousHand = new List<Card>(sameHand);
        var currentHand = new List<Card>(sameHand);
        previousHand.Add(cardToMove);

        var previousCombo = new List<Card>(sameCombo);
        var currentCombo = new List<Card>(sameCombo);
        currentCombo.Add(cardToMove);

        var previousState = new CardsState(sameDeck)
        {
            Combo = previousCombo,
            Deck = sameDeck,
            DiscardPile = sameDiscardPile,
            ExhaustPile = sameExhaustPile,
            Hand = previousHand,
            SpentPowerPile = sameSpentPowersPile
        };

        var currentState = new CardsState(sameDeck)
        {
            Combo = currentCombo,
            Deck = sameDeck,
            DiscardPile = sameDiscardPile,
            ExhaustPile = sameExhaustPile,
            Hand = currentHand,
            SpentPowerPile = sameSpentPowersPile
        };

        var expectedResult = new List<LocationOfCardThatHasMoved>()
        {
            new LocationOfCardThatHasMoved()
            {
                cardInPreviousState = cardToMove,
                cardInCurrentState = cardToMove,
                locationInPreviousState = new CardLocation() { location = CardLocation.LocationType.HAND, index = 2 },
                locationInCurrentState = new CardLocation() { location = CardLocation.LocationType.COMBO, index = 2 }
            }
        };

        var result = CardLocationCalculator.GetLocationsOfCardsThatMoved(previousState, currentState);

        Assert.That(result, Is.EquivalentTo(expectedResult));
    }
}
