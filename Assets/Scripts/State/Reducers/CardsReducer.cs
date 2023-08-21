using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using KitTraden.AnAbundanceOfMana.State.Actions;
using KitTraden.AnAbundanceOfMana.State.Entities;
using KitTraden.AnAbundanceOfMana.State.State;

namespace KitTraden.AnAbundanceOfMana.State.Reducers
{
    public class CardsReducer : Reducer<CardsState>
    {
        public override CardsState Reduce<TAction>(TAction action, CardsState state)
        {
            return action switch
            {
                AddInstanceOfCardToBottomOfDeckAction => AddInstanceOfCardToBottomOfDeck(action as AddInstanceOfCardToBottomOfDeckAction, state),
                AddInstanceOfCardToDiscardPileAction => AddInstanceOfCardToDiscardPile(action as AddInstanceOfCardToDiscardPileAction, state),
                AddInstanceOfCardToEndOfComboAction => AddInstanceOfCardToEndOfCombo(action as AddInstanceOfCardToEndOfComboAction, state),
                AddInstanceOfCardToHandAction => AddInstanceOfCardToHand(action as AddInstanceOfCardToHandAction, state),
                AddInstanceOfCardToTopOfDeckAction => AddInstanceOfCardToTopOfDeck(action as AddInstanceOfCardToTopOfDeckAction, state),
                DiscardComboAction => DiscardCombo(state),
                DiscardHandAction => DiscardHand(state),
                DiscardFromComboAction => DiscardFromCombo(action as DiscardFromComboAction, state),
                DiscardFromHandAction => DiscardFromHand(action as DiscardFromHandAction, state),
                DrawAction => Draw(action as DrawAction, state),
                ExhaustFromComboAction => ExhaustFromCombo(action as ExhaustFromComboAction, state),
                ExhaustFromDeckAction => ExhaustFromDeck(action as ExhaustFromDeckAction, state),
                ExhaustFromDiscardPileAction => ExhaustFromDiscardPile(action as ExhaustFromDiscardPileAction, state),
                ExhaustFromHandAction => ExhaustFromHand(action as ExhaustFromHandAction, state),
                PlayCardAndDiscardAction => PlayCardAndDiscard(action as PlayCardAndDiscardAction, state),
                PlayCardIntoComboAction => PlayCardIntoCombo(action as PlayCardIntoComboAction, state),
                PlayPowerCardAction => PlayPowerCard(action as PlayPowerCardAction, state),
                ReshuffleDiscardPileAction => ReshuffleDiscardPile(state),
                ShuffleDeckAction => ShuffleDeck(state),
                _ => state,// Deliberately do nothing
            };
        }

        private static List<Card> Shuffle(List<Card> cardsToShuffle)
        {
            var oldDeck = cardsToShuffle;
            var newDeck = new List<Card>(oldDeck.Count);

            while (oldDeck.Count > 0)
            {
                var index = Random.Range(0, oldDeck.Count);
                var card = oldDeck[index];
                oldDeck.RemoveAt(index);
                newDeck.Add(card);
            }

            return newDeck;
        }

        private CardsState AddInstanceOfCardToBottomOfDeck(AddInstanceOfCardToBottomOfDeckAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            newState.Deck.Add(action.Card);
            if (action.AddPermanently)
            {
                newState.PermamentDeck.Add(action.Card);
            }

            return newState;
        }

        private CardsState AddInstanceOfCardToDiscardPile(AddInstanceOfCardToDiscardPileAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            newState.DiscardPile.Add(action.Card);
            if (action.AddPermanently)
            {
                newState.PermamentDeck.Add(action.Card);
            }

            return newState;
        }

        private CardsState AddInstanceOfCardToEndOfCombo(AddInstanceOfCardToEndOfComboAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            newState.Combo.Add(action.Card);
            if (action.AddPermanently)
            {
                newState.PermamentDeck.Add(action.Card);
            }

            return newState;
        }

        private CardsState AddInstanceOfCardToHand(AddInstanceOfCardToHandAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            newState.Hand.Add(action.Card);
            if (action.AddPermanently)
            {
                newState.PermamentDeck.Add(action.Card);
            }

            return newState;
        }


        private CardsState AddInstanceOfCardToTopOfDeck(AddInstanceOfCardToTopOfDeckAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            newState.Deck.Insert(0, action.Card);
            if (action.AddPermanently)
            {
                newState.PermamentDeck.Add(action.Card);
            }

            return newState;
        }

        private CardsState DiscardCombo(CardsState state)
        {
            var newState = state.DeepCopy();

            newState.DiscardPile.AddRange(newState.Combo);
            newState.Combo.Clear();

            return newState;
        }

        private CardsState DiscardHand(CardsState state)
        {
            var newState = state.DeepCopy();

            newState.DiscardPile.AddRange(newState.Hand);
            newState.Hand.Clear();

            return newState;
        }

        private CardsState DiscardFromCombo(DiscardFromComboAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            var cardToDiscard = newState.Combo[action.comboIndexToDiscardFrom];
            newState.Combo.RemoveAt(action.comboIndexToDiscardFrom);
            newState.DiscardPile.Add(cardToDiscard);

            return newState;
        }

        private CardsState DiscardFromHand(DiscardFromHandAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            var cardToDiscard = newState.Hand[action.handIndexToDiscardFrom];
            newState.Hand.RemoveAt(action.handIndexToDiscardFrom);
            newState.DiscardPile.Add(cardToDiscard);

            return newState;
        }

        private CardsState Draw(DrawAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            int cardsToDraw = Math.Min(action.CardsToDraw, newState.Deck.Count);
            var cards = newState.Deck.GetRange(0, cardsToDraw);
            newState.Deck.RemoveRange(0, cardsToDraw);
            newState.Hand.AddRange(cards);

            return newState;
        }

        private CardsState ExhaustFromCombo(ExhaustFromComboAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            var cardToExhaust = newState.Combo[action.comboIndexToExhaustFrom];
            newState.Combo.RemoveAt(action.comboIndexToExhaustFrom);
            newState.ExhaustPile.Add(cardToExhaust);

            return newState;
        }

        private CardsState ExhaustFromDeck(ExhaustFromDeckAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            var cardToExhaust = newState.Deck[action.deckIndexToExhaustFrom];
            newState.Deck.RemoveAt(action.deckIndexToExhaustFrom);
            newState.ExhaustPile.Add(cardToExhaust);

            return newState;
        }

        private CardsState ExhaustFromDiscardPile(ExhaustFromDiscardPileAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            var cardToExhaust = newState.DiscardPile[action.discardIndexToExhaustFrom];
            newState.DiscardPile.RemoveAt(action.discardIndexToExhaustFrom);
            newState.ExhaustPile.Add(cardToExhaust);

            return newState;
        }

        private CardsState ExhaustFromHand(ExhaustFromHandAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            var cardToExhaust = newState.Hand[action.handIndexToExhaustFrom];
            newState.Hand.RemoveAt(action.handIndexToExhaustFrom);
            newState.ExhaustPile.Add(cardToExhaust);

            return newState;
        }

        private CardsState PlayCardAndDiscard(PlayCardAndDiscardAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            var cardToPlay = newState.Hand[action.handIndexToPlayFrom];
            newState.Hand.RemoveAt(action.handIndexToPlayFrom);
            newState.DiscardPile.Add(cardToPlay);

            return newState;
        }

        private CardsState PlayCardIntoCombo(PlayCardIntoComboAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            var cardToPlay = newState.Hand[action.handIndexToPlayFrom];
            newState.Hand.RemoveAt(action.handIndexToPlayFrom);

            if (action.comboIndexToPlayTo == newState.Combo.Count)
            {
                newState.Combo.Add(cardToPlay);
            }
            else
            {
                newState.Combo.Insert(action.comboIndexToPlayTo, cardToPlay);
            }

            return newState;
        }

        private CardsState PlayPowerCard(PlayPowerCardAction action, CardsState state)
        {
            var newState = state.DeepCopy();

            var cardToPlay = newState.Hand[action.handIndexToPlayFrom];
            newState.Hand.RemoveAt(action.handIndexToPlayFrom);
            newState.SpentPowerPile.Add(cardToPlay);

            return newState;
        }

        private CardsState ReshuffleDiscardPile(CardsState state)
        {
            // Adds the discard pile to the bottom of the draw pile
            // without shuffling the cards already in the discard pile
            var newState = state.DeepCopy();

            newState.DiscardPile = Shuffle(newState.DiscardPile);
            newState.Deck.AddRange(newState.DiscardPile);
            newState.DiscardPile.Clear();

            return newState;
        }

        private CardsState ShuffleDeck(CardsState state)
        {
            var newState = state.DeepCopy();
            newState.Deck = Shuffle(newState.Deck);
            return newState;
        }
    }
}