using System;
using System.Collections.Generic;
using System.Linq;
using KitTraden.AnAbundanceOfMana.MVC.Views;
using KitTraden.AnAbundanceOfMana.State.State;
using KitTraden.AnAbundanceOfMana.State.Entities;
using KitTraden.AnAbundanceOfMana.Calculators.Score;

namespace KitTraden.AnAbundanceOfMana.MVC
{
    public class StateToViewConverter
    {
        public static OverallView CovertStateToView(OverallState state)
        {
            return new OverallView(
                ConvertCardsState(state.cardsState),
                ConvertPlayerState(state.playerState),
                ConvertSpellState(state.spellState),
                CalculateScoreView(state)
            );
        }

        private static CardsView ConvertCardsState(CardsState state)
        {
            return new CardsView(
                ConvertListOfCards(state.PermamentDeck),
                ConvertListOfCards(state.Deck),
                ConvertListOfCards(state.Hand),
                ConvertListOfCards(state.Combo),
                ConvertListOfCards(state.DiscardPile),
                ConvertListOfCards(state.ExhaustPile),
                ConvertListOfCards(state.SpentPowerPile)
            );
        }

        private static SpellView ConvertSpellState(SpellState state)
        {
            var spellPhases = ConvertListOfSpellPhases(state.spell.Phases);

            return new SpellView(state.spell.Model, state.spell.Name, spellPhases, state.phaseIndex);
        }

        private static PlayerView ConvertPlayerState(PlayerState state)
        {
            return new PlayerView(
                state.MulligansRemaining,
                state.MaxMulligans,
                state.EnergyRemaining,
                state.EnergyPerTurn,
                state.StartOfTurnHandSize,
                state.MaxHandSize,
                state.StartOfTurnBaseMana,
                state.StartOfTurnMultiplier,
                state.CurrentTurnBaseManaBonus,
                state.CurrentTurnMultiplierBonusToAdd,
                state.CurrentTurnMultiplierBonusToMultiply
            );
        }

        private static List<CardView> ConvertListOfCards(List<Card> cards)
        {
            return cards.Select((state, _index) => ConvertCard(state)).ToList();
        }

        private static CardView ConvertCard(Card state)
        {
            var cost = ConvertEnergyCost(state.Cost);

            var type = state.Type switch
            {
                Card.CardType.COMBOABLE => CardView.CardType.COMBOABLE,
                Card.CardType.PLAYABLE => CardView.CardType.PLAYABLE,
                Card.CardType.POWER => CardView.CardType.POWER,
                Card.CardType.UNPLAYABLE => CardView.CardType.UNPLAYABLE,
                _ => throw new Exception($"Unknown CardType ${state.Type}")
            };

            return new CardView(
                state.Model,
                state.Name,
                state.Text,
                state.Sprite,
                type,
                cost,
                state.BaseMana,
                state.ToAddToMultiplier,
                state.ToMultiplyByMultiplier,
                state.OnPlayEffects
            );
        }

        private static EnergyCostView ConvertEnergyCost(EnergyCost state)
        {
            EnergyCostView.CostType costType = state.Type switch
            {
                EnergyCost.CostType.CONSTANT => EnergyCostView.CostType.CONSTANT,
                EnergyCost.CostType.X => EnergyCostView.CostType.X,
                _ => throw new Exception($"Unknown EnergyCostView.CostType of {state.Type}")
            };

            return new EnergyCostView(
                state.Model, state.ConstantCost, costType
            );
        }

        private static List<SpellPhaseView> ConvertListOfSpellPhases(List<SpellPhase> spellPhases)
        {
            return spellPhases.Select((state, _index) => ConvertSpellPhase(state)).ToList();
        }

        private static SpellPhaseView ConvertSpellPhase(SpellPhase state)
        {
            SpellPhaseView.MulliganBehaviorType mulliganBehvaior = state.MulliganBehavior switch
            {
                SpellPhase.MulliganBehaviorType.RESET => SpellPhaseView.MulliganBehaviorType.RESET,
                SpellPhase.MulliganBehaviorType.SKIP => SpellPhaseView.MulliganBehaviorType.SKIP,
                _ => throw new Exception($"Unkown SpellPhaseView.MulliganBehaviorType of {state.MulliganBehavior}")
            };

            return new SpellPhaseView(state.Model, state.ManaQuota, mulliganBehvaior);
        }

        private static ScoreView CalculateScoreView(OverallState state)
        {
            var scoreCalculation = ScoreCalculator.CalculateScore(state);
            return new ScoreView() { ScoreCalculation = scoreCalculation };
        }
    }
}