using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using KitTraden.AnAbundanceOfMana.Effects.CardEffects;
using System;
using Sirenix.Serialization;

namespace KitTraden.AnAbundanceOfMana.MVC.Models.Cards
{
    [CreateAssetMenu(fileName = "Card", menuName = "An Abundance of Mana/Card", order = 0)]
    public class Card : SerializedScriptableObject
    {
        public enum CardType
        {
            COMBOABLE,
            PLAYABLE,
            POWER,
            UNPLAYABLE
        }

        public string Name = "";
        public string Text = "";
        public Sprite Sprite;
        public CardType Type;

        [NonSerialized, OdinSerialize]
        public EnergyCost Cost = new() { ConstantCost = 1, Type = EnergyCost.CostType.CONSTANT };
        public int BaseMana = 0;
        public decimal ToAddToMultiplier = 0;
        public decimal ToMultiplyByMultiplier = 1;

        [NonSerialized, OdinSerialize]
        public List<CardEffect> OnPlayEffects = new();
        // TODO: Implement these
        // It's going to be a bit tricky since one effect can trigger a different
        // type of effect on another card, so I want to get effects working at all first.
        // public List<CardEffect> OnDiscardEffects;
        // public List<CardEffect> OnDrawEffects;
        // public List<CardEffect> OnInHandAtEndOfTurnEffects;
        // public List<CardEffect> OnExhaustEffects;
    }
}