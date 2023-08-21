using KitTraden.AnAbundanceOfMana.Calculators.CardLocation;

namespace KitTraden.AnAbundanceOfMana.MVC.Controllers
{
    public struct PlayResult
    {
        public CardEffectResult FinalCardEffectResult;
        public bool HasCardStayedInHand;
        public CardLocation FinalCardLocation;
    }
}