using System;
using KitTraden.AnAbundanceOfMana.State.Entities;
using ModelSpell = KitTraden.AnAbundanceOfMana.MVC.Models.Spells.Spell;

namespace KitTraden.AnAbundanceOfMana.State.State
{
    [Serializable]
    public class SpellState
    {
        public Spell spell { private set; get; }
        public int phaseIndex;

        public SpellState(Spell spell)
        {
            this.spell = spell;
            this.phaseIndex = 0;
        }

        public SpellState(ModelSpell modelSpell)
        {
            this.spell = new Spell(modelSpell);
            this.phaseIndex = 0;
        }

        public SpellState(SpellState spellStateToCopy)
        {
            this.spell = spellStateToCopy.spell;
            this.phaseIndex = spellStateToCopy.phaseIndex;
        }

        public SpellState DeepCopy()
        {
            return new SpellState(this);
        }
    }
}
