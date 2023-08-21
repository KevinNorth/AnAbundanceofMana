using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.State.State;

namespace KitTraden.AnAbundanceOfMana.Calculators.Score
{
    public class ScoreCalculator
    {
        public static ScoreCalculation CalculateScore(OverallState state)
        {
            decimal totalBaseMana = state.playerState.CurrentTurnBaseManaBonus;
            decimal multiplier = state.playerState.CurrentTurnMultiplierBonusToAdd
                                 * state.playerState.CurrentTurnMultiplierBonusToMultiply;

            List<ScoreCalculationStep> calculationSteps = new();

            foreach (var card in state.cardsState.Combo)
            {
                var step = new ScoreCalculationStep
                {
                    PreviousBaseMana = totalBaseMana,
                    PreviousMultiplier = multiplier,
                    PreviousScore = totalBaseMana * multiplier,
                    ToAddToBaseMana = card.BaseMana,
                    ToAddToMultiplier = card.ToAddToMultiplier,
                    ToMultiplyByMultiplier = card.ToMultiplyByMultiplier
                };

                totalBaseMana += card.BaseMana;
                multiplier += card.ToAddToMultiplier;
                multiplier *= card.ToMultiplyByMultiplier;

                step.CurrentBaseMana = totalBaseMana;
                step.CurrentMultiplier = multiplier;
                step.CurrentScore = totalBaseMana * multiplier;

                calculationSteps.Add(step);
            }

            var finalScore = totalBaseMana * multiplier;

            return new ScoreCalculation()
            {
                StartingBaseMana = state.playerState.CurrentTurnBaseManaBonus,
                StartingMultiplierToAdd = state.playerState.CurrentTurnBaseManaBonus,
                StartingMultiplierToMultiply = state.playerState.CurrentTurnMultiplierBonusToMultiply,
                CalculationSteps = calculationSteps,
                FinalBaseMana = totalBaseMana,
                FinalMultiplier = multiplier,
                FinalScore = finalScore
            };
        }
    }
}