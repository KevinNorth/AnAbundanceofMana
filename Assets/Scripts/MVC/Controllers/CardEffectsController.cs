using System;
using UnityEngine;
using KitTraden.AnAbundanceOfMana.Calculators.CardLocation;
using KitTraden.AnAbundanceOfMana.Effects.CardEffects;
using KitTraden.AnAbundanceOfMana.State;
using KitTraden.AnAbundanceOfMana.State.Actions;
using KitTraden.AnAbundanceOfMana.State.Entities;
using KitTraden.AnAbundanceOfMana.State.State;

namespace KitTraden.AnAbundanceOfMana.MVC.Controllers
{
    public class CardEffectsController
    {
        private readonly Store Store;

        private OverallState State { get => Store.GetState(); }
        private CardsState CardsState { get => State.cardsState; }
        private PlayerState PlayerState { get => State.playerState; }
        private SpellState SpellState { get => State.spellState; }

        public CardEffectsController(Store store)
        {
            Store = store;
        }

        public PlayResult HandleOnPlayEffects(Card card, CardLocation cardLocation)
        {
            bool cardHasStayedInHand = true;

            CardEffectResult lastCardEffectResult = CardEffectResult.OK;

            foreach (var effect in card.OnPlayEffects)
            {
                lastCardEffectResult = effect switch
                {
                    AddEnergyEffect => AddEnergy(effect as AddEnergyEffect),
                    AddEnergyPerTurnEffect => AddEnergyPerTurn(effect as AddEnergyPerTurnEffect),
                    AddInstanceOfCardToBottomOfDeckEffect => AddInstanceOfCardToBottomOfDeck(effect as AddInstanceOfCardToBottomOfDeckEffect),
                    AddInstanceOfCardToDiscardPileEffect => AddInstanceOfCardToDiscardPile(effect as AddInstanceOfCardToDiscardPileEffect),
                    AddInstanceOfCardToEndOfComboEffect => AddInstanceOfCardToEndOfCombo(effect as AddInstanceOfCardToEndOfComboEffect),
                    AddInstanceOfCardToHandEffect => AddInstanceOfCardToHand(effect as AddInstanceOfCardToHandEffect),
                    AddInstanceOfCardToTopOfDeckEffect => AddInstanceOfCardToTopOfDeck(effect as AddInstanceOfCardToTopOfDeckEffect),
                    AddMulligansEffect => AddMulligans(effect as AddMulligansEffect),
                    ChangeBaseManaEffect => ChangeBaseMana(effect as ChangeBaseManaEffect),
                    ChangeMaximumHandSizeEffect => ChangeMaximumHandSize(effect as ChangeMaximumHandSizeEffect),
                    ChangeMaximumMulligansEffect => ChangeMaximumMulligans(effect as ChangeMaximumMulligansEffect),
                    ChangeMultiplierLinearlyEffect => ChangeMultiplierLinearly(effect as ChangeMultiplierLinearlyEffect),
                    ChangeMultiplierMultiplicitavelyEffect => ChangeMultiplierMultiplicitavely(effect as ChangeMultiplierMultiplicitavelyEffect),
                    ChangeStartOfTurnBaseManaEffect => ChangeStartOfTurnBaseMana(effect as ChangeStartOfTurnBaseManaEffect),
                    ChangeStartOfTurnBaseMultiplierEffect => ChangeStartOfTurnBaseMultiplier(effect as ChangeStartOfTurnBaseMultiplierEffect),
                    ChangeStartOfTurnHandSizeEffect => ChangeStartOfTurnHandSize(effect as ChangeStartOfTurnHandSizeEffect),
                    DiscardCardEffect => DiscardCard(effect as DiscardCardEffect, card),
                    DiscardComboEffect => DiscardCombo(),
                    DiscardFromComboEffect => DiscardFromCombo(effect as DiscardFromComboEffect, card),
                    DiscardHandAndRedrawEffect => DiscardHandAndRedraw(effect as DiscardHandAndRedrawEffect),
                    DrawCardsEffect => DrawCards(effect as DrawCardsEffect),
                    ExhaustEffect => Exhaust(card),
                    LoseMulligansEffect => LoseMulligans(effect as LoseMulligansEffect),
                    ReshuffleDiscardPileEffect => ReshuffleDiscardPile(),
                    SetMaximumMulligansEffect => SetMaximumMulligans(effect as SetMaximumMulligansEffect),
                    SetStartofTurnBaseManaEffect => SetStartofTurnBaseMana(effect as SetStartofTurnBaseManaEffect),
                    SetStartOfTurnHandSizeEffect => SetStartOfTurnHandSize(effect as SetStartOfTurnHandSizeEffect),
                    SetStartofTurnMultiplierEffect => SetStartofTurnMultiplier(effect as SetStartofTurnMultiplierEffect),
                    ShuffleDeckEffect => ShuffleDeck(),
                    _ => throw new Exception($"Unknown CardEffect ${effect}")
                };

                if (lastCardEffectResult.HasPlayBeenCancelled)
                {
                    break;
                }

                if (cardHasStayedInHand && CardLocationCalculator.GetCardLocation(card, CardsState).location != CardLocation.LocationType.HAND)
                {
                    cardHasStayedInHand = false;
                }
            }

            var result = new PlayResult
            {
                FinalCardEffectResult = lastCardEffectResult,
                HasCardStayedInHand = cardHasStayedInHand,
                FinalCardLocation = cardLocation
            };

            return result;
        }

        private CardEffectResult AddEnergy(AddEnergyEffect effect)
        {
            Store.Dispatch(new GainEnergyAction() { EnergyToGain = effect.EnergyToGain });
            return CardEffectResult.OK;
        }

        private CardEffectResult AddEnergyPerTurn(AddEnergyPerTurnEffect effect)
        {
            Store.Dispatch(new GainEnergyPerTurnAction()
            { EnergyPerTurnToGain = effect.EnergyPerTurnToGain }
            );
            return CardEffectResult.OK;
        }

        private CardEffectResult AddInstanceOfCardToBottomOfDeck(AddInstanceOfCardToBottomOfDeckEffect effect)
        {
            var newCard = new Card(effect.Card);
            Store.Dispatch(new AddInstanceOfCardToBottomOfDeckAction()
            {
                AddPermanently = effect.AddPermanently,
                Card = newCard
            });
            return CardEffectResult.OK;
        }

        private CardEffectResult AddInstanceOfCardToDiscardPile(AddInstanceOfCardToDiscardPileEffect effect)
        {
            var newCard = new Card(effect.Card);
            Store.Dispatch(new AddInstanceOfCardToDiscardPileAction()
            {
                AddPermanently = effect.AddPermanently,
                Card = newCard
            });
            return CardEffectResult.OK;
        }

        private CardEffectResult AddInstanceOfCardToEndOfCombo(AddInstanceOfCardToEndOfComboEffect effect)
        {
            var newCard = new Card(effect.Card);
            Store.Dispatch(new AddInstanceOfCardToEndOfComboAction()
            {
                AddPermanently = effect.AddPermanently,
                Card = newCard
            });
            return CardEffectResult.OK;
        }

        private CardEffectResult AddInstanceOfCardToHand(AddInstanceOfCardToHandEffect effect)
        {
            var newCard = new Card(effect.Card);
            Store.Dispatch(new AddInstanceOfCardToHandAction()
            {
                AddPermanently = effect.AddPermanently,
                Card = newCard
            });
            return CardEffectResult.OK;
        }

        private CardEffectResult AddInstanceOfCardToTopOfDeck(AddInstanceOfCardToTopOfDeckEffect effect)
        {
            var newCard = new Card(effect.Card);
            Store.Dispatch(new AddInstanceOfCardToTopOfDeckAction()
            {
                AddPermanently = effect.AddPermanently,
                Card = newCard
            });
            return CardEffectResult.OK;
        }

        private CardEffectResult AddMulligans(AddMulligansEffect effect)
        {
            Store.Dispatch(new GainMultipleMulligansAction() { MulligansToGain = effect.MulligansToAdd });
            return CardEffectResult.OK;
        }

        private CardEffectResult ChangeBaseMana(ChangeBaseManaEffect effect)
        {
            if (effect.DeltaBaseMana == 0)
            {
                return CardEffectResult.OK;
            }
            else if (effect.DeltaBaseMana > 0)
            {
                Store.Dispatch(new GainBonusBaseManaAction() { BonusBaseManaToGain = effect.DeltaBaseMana });
            }
            else
            {
                Store.Dispatch(new LoseBonusBaseManaAction() { BonusBaseManaToLose = -effect.DeltaBaseMana });
            }

            return CardEffectResult.OK;
        }

        private CardEffectResult ChangeMaximumHandSize(ChangeMaximumHandSizeEffect effect)
        {
            int newMaxHandSize = Math.Max(
                PlayerState.MaxHandSize + effect.DeltaMaximumHandSize,
                effect.MinimumMaxHandSize
            );
            Store.Dispatch(new SetMaxHandSizeAction() { MaxHandSize = newMaxHandSize });
            return CardEffectResult.OK;
        }

        private CardEffectResult ChangeMaximumMulligans(ChangeMaximumMulligansEffect effect)
        {
            int newMaxMulligans = Math.Max(
                PlayerState.MaxMulligans + effect.DeltaMaximumMulligans,
                effect.MinimumMaxMulligans
            );
            Store.Dispatch(new SetMaxMulligansAction() { MaxMulligans = newMaxMulligans });
            return CardEffectResult.OK;
        }

        private CardEffectResult ChangeMultiplierLinearly(ChangeMultiplierLinearlyEffect effect)
        {
            if (effect.DeltaMultiplier == 0)
            {
                return CardEffectResult.OK;
            }
            else if (effect.DeltaMultiplier > 0)
            {
                Store.Dispatch(new GainBonusMutliplierToAddAction() { BonusMutiplierToAddToGain = effect.DeltaMultiplier });
            }
            else
            {
                Store.Dispatch(new LoseBonusMutliplierToAddAction() { BonusMutiplierToAddToLose = -effect.DeltaMultiplier });
            }
            return CardEffectResult.OK;
        }

        private CardEffectResult ChangeMultiplierMultiplicitavely(ChangeMultiplierMultiplicitavelyEffect effect)
        {
            Store.Dispatch(new ApplyBonusMutliplierToMultiplyAction() { BonusMutiplierToMultiplyToApply = effect.MultiplierFactor });
            return CardEffectResult.OK;
        }

        private CardEffectResult ChangeStartOfTurnBaseMana(ChangeStartOfTurnBaseManaEffect effect)
        {
            var newBaseMana = PlayerState.StartOfTurnBaseMana + effect.DeltaStartOfTurnBaseMana;
            Store.Dispatch(new SetStartofTurnBaseManaAction() { StartOfTurnBaseMana = newBaseMana });
            return CardEffectResult.OK;
        }

        private CardEffectResult ChangeStartOfTurnBaseMultiplier(ChangeStartOfTurnBaseMultiplierEffect effect)
        {
            var newBaseMultiplier = PlayerState.StartOfTurnMultiplier + effect.DeltaStartOfTurnMultiplier;
            Store.Dispatch(new SetStartofTurnMultiplierAction() { StartOfTurnMultiplier = newBaseMultiplier });
            return CardEffectResult.OK;
        }

        private CardEffectResult ChangeStartOfTurnHandSize(ChangeStartOfTurnHandSizeEffect effect)
        {
            var newHandSize = Math.Max(
                PlayerState.StartOfTurnHandSize + effect.DeltaStartOfTurnHandSize,
                effect.MinimumMaxHandSize
            );
            Store.Dispatch(new SetStartOfTurnHandSizeAction() { StartOfTurnHandSize = newHandSize });
            return CardEffectResult.OK;
        }

        private CardEffectResult DiscardCard(DiscardCardEffect effect, Card card)
        {
            var cardLocation = CardLocationCalculator.GetCardLocation(card, CardsState);
            bool thisCardIsInHand = cardLocation.location == CardLocation.LocationType.HAND;

            if (CardsState.Hand.Count <= (thisCardIsInHand ? 1 : 0))
            {
                return CardEffectResult.OK;
            }

            int index = Math.Min(effect.IndexToDiscard, CardsState.Hand.Count);
            if (effect.CountFromEndOfHand)
            {
                index = Math.Max(CardsState.Hand.Count - (effect.IndexToDiscard + 1), 0);
            }

            // Make sure we don't try to discard the card being processed
            if (thisCardIsInHand && index == cardLocation.index)
            {
                if (effect.CountFromEndOfHand)
                {
                    index -= 1;

                    if (index < 0)
                    {
                        index = cardLocation.index + 1;
                    }
                }
                else
                {
                    index += 1;

                    // Make sure we don't try to discard outside the hand's size
                    if (index >= CardsState.Hand.Count)
                    {
                        index = cardLocation.index - 1;
                    }
                }
            }

            Store.Dispatch(new DiscardFromHandAction() { handIndexToDiscardFrom = index });
            return CardEffectResult.OK;
        }

        private CardEffectResult DiscardCombo()
        {
            Store.Dispatch(new DiscardComboAction());
            return CardEffectResult.OK;
        }

        private CardEffectResult DiscardFromCombo(DiscardFromComboEffect effect, Card card)
        {
            var cardLocation = CardLocationCalculator.GetCardLocation(card, CardsState);
            bool thisCardIsInCombo = cardLocation.location == CardLocation.LocationType.COMBO;

            if (CardsState.Combo.Count <= (thisCardIsInCombo ? 1 : 0))
            {
                return CardEffectResult.OK;
            }

            int index = Math.Min(effect.CardIndex, CardsState.Hand.Count);
            if (effect.CountFromEndOfCombo)
            {
                index = Math.Max(CardsState.Combo.Count - (effect.CardIndex + 1), 0);
            }

            // Make sure we don't try to discard the card being processed
            if (thisCardIsInCombo && index == cardLocation.index)
            {
                if (effect.CountFromEndOfCombo)
                {
                    index -= 1;

                    if (index < 0)
                    {
                        index = cardLocation.index + 1;
                    }
                }
                else
                {
                    index += 1;

                    // Make sure we don't try to discard outside the hand's size
                    if (index >= CardsState.Hand.Count)
                    {
                        index = cardLocation.index - 1;
                    }
                }
            }

            Store.Dispatch(new DiscardFromComboAction() { comboIndexToDiscardFrom = index });
            return CardEffectResult.OK;
        }

        private CardEffectResult DiscardHandAndRedraw(DiscardHandAndRedrawEffect effect)
        {
            Store.Dispatch(new DiscardHandAction());

            if (effect.UseStartOfTurnHandSize)
            {
                return DrawCards(new DrawCardsEffect() { NumCardsToDraw = PlayerState.StartOfTurnHandSize });
            }
            else
            {
                return DrawCards(new DrawCardsEffect() { NumCardsToDraw = effect.NumCardsToDraw });
            }
        }

        private CardEffectResult DrawCards(DrawCardsEffect effect)
        {
            int numCardsToDraw = Math.Min(
                effect.NumCardsToDraw,
                PlayerState.MaxHandSize - CardsState.Hand.Count
            );

            if (numCardsToDraw > CardsState.Deck.Count)
            {
                Store.Dispatch(new ReshuffleDiscardPileAction());

                // If the deck still doesn't have enough cards after
                // shuffling in the discard pile, draw all cards.
                if (numCardsToDraw > CardsState.Deck.Count)
                {
                    numCardsToDraw = CardsState.Deck.Count;
                }

                // In the rare situation both the deck and discard pile are empty,
                // we can return early.
                if (numCardsToDraw == 0)
                {
                    return CardEffectResult.OK;
                }
            }

            Store.Dispatch(new DrawAction() { CardsToDraw = effect.NumCardsToDraw });

            return CardEffectResult.OK;
        }

        private CardEffectResult Exhaust(Card card)
        {
            var cardLocation = CardLocationCalculator.GetCardLocation(card, CardsState);

            Debug.Log($"{cardLocation.location} {cardLocation.index}");

            switch (cardLocation.location)
            {
                case CardLocation.LocationType.HAND:
                    Store.Dispatch(new ExhaustFromHandAction() { handIndexToExhaustFrom = cardLocation.index });
                    break;
                case CardLocation.LocationType.COMBO:
                    Store.Dispatch(new ExhaustFromComboAction() { comboIndexToExhaustFrom = cardLocation.index });
                    break;
                case CardLocation.LocationType.DECK:
                    Store.Dispatch(new ExhaustFromDeckAction() { deckIndexToExhaustFrom = cardLocation.index });
                    break;
                case CardLocation.LocationType.DISCARD_PILE:
                    Store.Dispatch(new ExhaustFromDiscardPileAction() { discardIndexToExhaustFrom = cardLocation.index });
                    break;
                default:
                    break;
            }

            return CardEffectResult.OK;
        }

        private CardEffectResult LoseMulligans(LoseMulligansEffect effect)
        {
            if (!effect.CanPlayWithoutEnoughMulligans && effect.MulligansToLose >= PlayerState.MulligansRemaining)
            {
                return new CardEffectResult()
                {
                    HasPlayBeenCancelled = true,
                    ToastText = "Without any remaining assays, playing this card would kill me."
                };
            }

            int mulligansToLose = Math.Min(effect.MulligansToLose, PlayerState.MulligansRemaining - 1);

            for (int i = 0; i < mulligansToLose; i++)
            {
                Store.Dispatch(new LoseMulliganAction());
            }

            return CardEffectResult.OK;
        }

        private CardEffectResult ReshuffleDiscardPile()
        {
            Store.Dispatch(new ReshuffleDiscardPileAction());

            return CardEffectResult.OK;
        }

        private CardEffectResult SetMaximumMulligans(SetMaximumMulligansEffect effect)
        {
            Store.Dispatch(new SetMaxMulligansAction() { MaxMulligans = effect.MaximumMulligans });
            return CardEffectResult.OK;
        }

        private CardEffectResult SetStartofTurnBaseMana(SetStartofTurnBaseManaEffect effect)
        {
            Store.Dispatch(new SetStartofTurnBaseManaAction() { StartOfTurnBaseMana = effect.StartOfTurnBaseMana });
            return CardEffectResult.OK;
        }

        private CardEffectResult SetStartOfTurnHandSize(SetStartOfTurnHandSizeEffect effect)
        {
            Store.Dispatch(new SetStartOfTurnHandSizeAction() { StartOfTurnHandSize = effect.StartOfTurnHandSize });
            return CardEffectResult.OK;
        }

        private CardEffectResult SetStartofTurnMultiplier(SetStartofTurnMultiplierEffect effect)
        {
            Store.Dispatch(new SetStartofTurnMultiplierAction() { StartOfTurnMultiplier = effect.StartOfTurnMultiplier });
            return CardEffectResult.OK;
        }

        private CardEffectResult ShuffleDeck()
        {
            Store.Dispatch(new ShuffleDeckAction());

            return CardEffectResult.OK;
        }
    }
}