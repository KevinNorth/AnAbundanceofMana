using System;
using UnityEngine;
using KitTraden.AnAbundanceOfMana.Calculators.CardLocation;
using KitTraden.AnAbundanceOfMana.State;
using KitTraden.AnAbundanceOfMana.State.State;
using KitTraden.AnAbundanceOfMana.State.Actions;
using KitTraden.AnAbundanceOfMana.UI.Events;
using KitTraden.AnAbundanceOfMana.UI.Feedback;
using KitTraden.AnAbundanceOfMana.State.Entities;
using System.Collections.Generic;
using Action = KitTraden.AnAbundanceOfMana.State.Actions.Action;

namespace KitTraden.AnAbundanceOfMana.MVC.Controllers
{
    public class Controller
    {
        public OverallState State { get => store.GetState(); }
        public CardsState CardsState { get => State.cardsState; }
        public PlayerState PlayerState { get => State.playerState; }
        public SpellState SpellState { get => State.spellState; }
        public IReadOnlyList<Action> PreviousActions { get => store.GetHistory(); }
        public IReadOnlyList<OverallState> PreviousStates { get => store.GetPreviousStates(); }

        private readonly Store store;
        private readonly CardEffectsController CardEffectsController;

        public Controller(Store store)
        {
            this.store = store;
            CardEffectsController = new CardEffectsController(store);
        }

        public Feedback HandleUIEvent(UIEvent uiEvent)
        {
            switch (uiEvent)
            {
                case EndTurnEvent: EndTurn(); return Feedback.OK;
                case PlayCardEvent: return PlayCard(uiEvent as PlayCardEvent);
                case StartSpellEvent: StartSpell(); return Feedback.OK;
                default: throw new Exception($"Do not know how to process a UIEvent of type {uiEvent.GetType()}");
            };
        }

        public void RestoreState(int index)
        {
            store.RestoreState(index);
        }

        private void EndTurn()
        {
            DiscardHand();
            DiscardCombo();

            var nextPhaseIndex = SpellState.phaseIndex + 1;
            var phaseCount = SpellState.spell.Phases.Count;
            if (nextPhaseIndex < phaseCount)
            {
                store.Dispatch(new AdvancePhaseAction());
                StartTurn();
            }
            else
            {
                EndSpell();
            }
        }

        private Feedback PlayCard(PlayCardEvent uiEvent)
        {
            int stateIndexBeforePlayingCard = store.GetHistory().Count - 1;

            var cardToPlay = CardsState.Hand[uiEvent.CardIndex];

            if (cardToPlay.Type == Card.CardType.UNPLAYABLE)
            {
                return Feedback.CANNOT_PLAY_UNPLAYABLE_CARDS;
            }

            var cost = cardToPlay.Cost.ConstantCost;

            if (cardToPlay.Cost.Type == EnergyCost.CostType.X)
            {
                cost = PlayerState.EnergyRemaining;
            }

            if (cost > PlayerState.EnergyRemaining)
            {
                return Feedback.NOT_ENOUGH_ENERGY;
            }

            store.Dispatch(new LoseEnergyAction { EnergyToLose = cost });

            var startingCardLocation = new CardLocation
            {
                index = uiEvent.CardIndex,
                location = CardLocation.LocationType.HAND
            };
            var playResult = CardEffectsController.HandleOnPlayEffects(cardToPlay, startingCardLocation);
            Debug.Log(JsonUtility.ToJson(playResult));

            if (playResult.FinalCardEffectResult.HasPlayBeenCancelled)
            {
                store.RestoreState(stateIndexBeforePlayingCard);
                return new Feedback()
                {
                    HasPlayBeenCancelled = true,
                    ToastText = playResult.FinalCardEffectResult.ToastText
                };
            }

            bool shouldCardBeMovedFromHand =
                playResult.FinalCardLocation.location == CardLocation.LocationType.HAND
                && playResult.HasCardStayedInHand;

            if (shouldCardBeMovedFromHand)
            {
                switch (cardToPlay.Type)
                {
                    case Card.CardType.COMBOABLE:
                        store.Dispatch(new PlayCardIntoComboAction
                        {
                            handIndexToPlayFrom = uiEvent.CardIndex,
                            comboIndexToPlayTo = CardsState.Combo.Count
                        });
                        break;
                    case Card.CardType.PLAYABLE:
                        store.Dispatch(new PlayCardAndDiscardAction
                        {
                            handIndexToPlayFrom = uiEvent.CardIndex
                        });
                        break;
                    case Card.CardType.POWER:
                        store.Dispatch(new PlayPowerCardAction
                        {
                            handIndexToPlayFrom = uiEvent.CardIndex
                        });
                        break;
                    default:
                        break;
                }
            }

            return new Feedback()
            {
                HasPlayBeenCancelled = false,
                ToastText = playResult.FinalCardEffectResult.ToastText
            };
        }

        private void StartSpell()
        {
            StartTurn();
        }

        private void EndSpell()
        {
            // Not yet implemented
        }

        private void StartTurn()
        {
            store.Dispatch(new ShuffleDeckAction());
            DrawCards(PlayerState.StartOfTurnHandSize);
            store.Dispatch(new SetEnergyAction() { Energy = PlayerState.EnergyPerTurn });
            store.Dispatch(new ResetCurrentBonusesAction());
        }

        private void DrawCards(int cardsToDraw)
        {
            for (int i = 0; i < cardsToDraw; i++)
            {
                if (CardsState.Hand.Count >= PlayerState.MaxHandSize)
                {
                    break;
                }

                if (CardsState.Deck.Count == 0)
                {
                    if (CardsState.DiscardPile.Count == 0)
                    {
                        break;
                    }

                    store.Dispatch(new ReshuffleDiscardPileAction());
                }

                store.Dispatch(new DrawAction());
            }
        }

        private void DiscardHand()
        {
            store.Dispatch(new DiscardHandAction());
        }

        private void DiscardCombo()
        {
            store.Dispatch(new DiscardComboAction());
        }
    }
}