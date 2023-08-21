using System.Collections.Generic;

namespace KitTraden.AnAbundanceOfMana.Calculators.Score
{
    public struct ScoreCalculation
    {
        public int StartingBaseMana;
        public decimal StartingMultiplierToAdd;
        public decimal StartingMultiplierToMultiply;

        public List<ScoreCalculationStep> CalculationSteps;

        public decimal FinalBaseMana;
        public decimal FinalMultiplier;
        public decimal FinalScore;
    }
}