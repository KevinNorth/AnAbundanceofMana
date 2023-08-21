using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace KitTraden.AnAbundanceOfMana.MVC.Models.Spells
{
    [CreateAssetMenu(fileName = "Spell", menuName = "An Abundance of Mana/Spell", order = 0)]
    public class Spell : SerializedScriptableObject
    {
        public string Name = "";
        [NonSerialized, OdinSerialize]
        public List<SpellPhase> Phases = new();
    }
}