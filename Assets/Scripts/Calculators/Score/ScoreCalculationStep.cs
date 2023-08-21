namespace KitTraden.AnAbundanceOfMana.Calculators.Score
{
    public struct ScoreCalculationStep
    {
        public int CardIndex;
        public decimal PreviousBaseMana;
        public decimal PreviousMultiplier;
        public decimal PreviousScore;
        public decimal CurrentBaseMana;
        public decimal ToAddToBaseMana;
        public decimal ToAddToMultiplier;
        public decimal ToMultiplyByMultiplier;
        public decimal CurrentMultiplier;
        public decimal CurrentScore;
    }
}