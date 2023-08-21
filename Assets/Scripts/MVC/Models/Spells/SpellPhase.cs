namespace KitTraden.AnAbundanceOfMana.MVC.Models.Spells
{
    public class SpellPhase
    {
        public enum MulliganBehaviorType
        {
            SKIP,
            RESET
        }

        public decimal ManaQuota = 0;
        public MulliganBehaviorType MulliganBehavior;
    }
}
