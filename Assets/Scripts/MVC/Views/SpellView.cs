using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ModelSpell = KitTraden.AnAbundanceOfMana.MVC.Models.Spells.Spell;

namespace KitTraden.AnAbundanceOfMana.MVC.Views
{
    public class SpellView
    {
        public ModelSpell Model;
        public string Name = "";
        public List<SpellPhaseView> Phases;
        public int PhaseIndex;

        public SpellView(ModelSpell model, int phaseIndex = 0)
        {
            Model = model;
            Name = model.Name;
            Phases = model.Phases.Select((modelPhase, _index) => new SpellPhaseView(modelPhase)).ToList();
            PhaseIndex = phaseIndex;
        }

        public SpellView(SpellView spellToCopy)
        {
            Model = spellToCopy.Model;
            Name = spellToCopy.Name;
            Phases = spellToCopy.Phases.Select((phase, _index) => new SpellPhaseView(phase)).ToList();
            PhaseIndex = spellToCopy.PhaseIndex;
        }

        public SpellView(ModelSpell model, string name, List<SpellPhaseView> phases, int phaseIndex)
        {
            Model = model;
            Name = name;
            Phases = phases;
            PhaseIndex = phaseIndex;
        }
    }
}