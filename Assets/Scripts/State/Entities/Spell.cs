using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ModelSpell = KitTraden.AnAbundanceOfMana.MVC.Models.Spells.Spell;

namespace KitTraden.AnAbundanceOfMana.State.Entities
{
    [Serializable]
    public class Spell
    {
        public ModelSpell Model;
        public string Name = "";
        public List<SpellPhase> Phases;

        public Spell(ModelSpell model)
        {
            Model = model;
            Name = model.Name;
            Phases = model.Phases.Select((modelPhase, _index) => new SpellPhase(modelPhase)).ToList();
        }

        public Spell(Spell spellToCopy)
        {
            Model = spellToCopy.Model;
            Name = spellToCopy.Name;
            Phases = spellToCopy.Phases.Select((phase, _index) => new SpellPhase(phase)).ToList();
        }

        public Spell(ModelSpell model, string name, List<SpellPhase> phases)
        {
            Model = model;
            Name = name;
            Phases = phases;
        }
    }
}