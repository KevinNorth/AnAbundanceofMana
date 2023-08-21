using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.Effects.CardEffects;
using KitTraden.AnAbundanceOfMana.State.Entities;

namespace KitTraden.AnAbundanceOfMana.State.Actions
{
    public class AddOnPlayCardEffectsToQueueAction : Action
    {
        public Card Card;
    }

    public class MarkCurrentCardEffectAsCompletedAction : Action { }
}