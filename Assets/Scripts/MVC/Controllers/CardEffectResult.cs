using KitTraden.AnAbundanceOfMana.MVC.Models.Cards;
using NaughtyAttributes;

namespace KitTraden.AnAbundanceOfMana.MVC.Controllers
{
    public struct CardEffectResult
    {
        public static CardEffectResult OK = new()
        {
            HasPlayBeenCancelled = false,
            ToastText = null
        };

        public bool HasPlayBeenCancelled;
        public string ToastText;
    }
}