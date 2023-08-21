using UnityEngine;
using ModelPhase = KitTraden.AnAbundanceOfMana.MVC.Models.Spells.SpellPhase;

namespace KitTraden.AnAbundanceOfMana.MVC.Views
{
    public class SpellPhaseView
    {
        public enum MulliganBehaviorType
        {
            SKIP,
            RESET
        }

        public ModelPhase Model;
        public decimal ManaQuota;
        public MulliganBehaviorType MulliganBehavior;

        public SpellPhaseView(ModelPhase model)
        {
            Model = model;
            ManaQuota = model.ManaQuota;

            switch (model.MulliganBehavior)
            {
                case ModelPhase.MulliganBehaviorType.SKIP:
                    MulliganBehavior = MulliganBehaviorType.SKIP;
                    break;
                case ModelPhase.MulliganBehaviorType.RESET:
                    MulliganBehavior = MulliganBehaviorType.RESET;
                    break;
                default:
                    throw new System.Exception($"Unknown MullgianBehaviorType {model.MulliganBehavior}");
            }
        }

        public SpellPhaseView(SpellPhaseView spellPhaseToCopy)
        {
            Model = spellPhaseToCopy.Model;
            ManaQuota = spellPhaseToCopy.ManaQuota;
            MulliganBehavior = spellPhaseToCopy.MulliganBehavior;
        }

        public SpellPhaseView(ModelPhase model, decimal manaQuota, MulliganBehaviorType mulliganBehavior)
        {
            Model = model;
            ManaQuota = manaQuota;
            MulliganBehavior = mulliganBehavior;
        }
    }
}
