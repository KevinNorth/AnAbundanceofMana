using System;
using System.Collections.Generic;
using UnityEngine;
using KitTraden.AnAbundanceOfMana.Effects.CardEffects;
using CardModel = KitTraden.AnAbundanceOfMana.MVC.Models.Cards.Card;

namespace KitTraden.AnAbundanceOfMana.State.Entities
{
    [Serializable]
    public class Card : IEquatable<Card>
    {
        public enum CardType
        {
            COMBOABLE,
            PLAYABLE,
            POWER,
            UNPLAYABLE
        }

        public CardModel Model;
        public string Name = "";
        public string Text = "";
        public Sprite Sprite;
        public CardType Type;

        public EnergyCost Cost;
        public int BaseMana = 0;
        public decimal ToAddToMultiplier = 0;
        public decimal ToMultiplyByMultiplier = 1;

        // Used to keep track of individual cards even as additional
        // instances are created between States. Two copies of the same
        // original card can have different UUIDs and so be tracked
        // separately.
        public string UUID;

        public List<CardEffect> OnPlayEffects;

        public Card(CardModel model)
        {
            var type = model.Type switch
            {
                CardModel.CardType.COMBOABLE => CardType.COMBOABLE,
                CardModel.CardType.PLAYABLE => CardType.PLAYABLE,
                CardModel.CardType.POWER => CardType.POWER,
                CardModel.CardType.UNPLAYABLE => CardType.UNPLAYABLE,
                _ => throw new System.Exception($"Unknown CardType ${model.Type}")
            };

            Model = model;
            Name = model.Name;
            Text = model.Text;
            Sprite = model.Sprite;
            Type = type;
            Cost = new EnergyCost(model.Cost);
            BaseMana = model.BaseMana;
            ToAddToMultiplier = model.ToAddToMultiplier;
            ToMultiplyByMultiplier = model.ToMultiplyByMultiplier;
            OnPlayEffects = model.OnPlayEffects;
            UUID = Guid.NewGuid().ToString();
        }
        public Card(Card cardToCopy, bool copyGUID = true)
        {
            Model = cardToCopy.Model;
            Name = cardToCopy.Name;
            Text = cardToCopy.Text;
            Sprite = cardToCopy.Sprite;
            Type = cardToCopy.Type;
            Cost = cardToCopy.Cost;
            BaseMana = cardToCopy.BaseMana;
            ToAddToMultiplier = cardToCopy.ToAddToMultiplier;
            ToMultiplyByMultiplier = cardToCopy.ToMultiplyByMultiplier;
            OnPlayEffects = cardToCopy.OnPlayEffects;
            UUID = copyGUID ? cardToCopy.UUID : Guid.NewGuid().ToString();
        }

        public Card(
            CardModel model,
            string name,
            string text,
            Sprite sprite,
            CardType type,
            EnergyCost cost,
            int baseMana,
            decimal toAddToMultiplier,
            decimal toMultiplyByMultiplier,
            List<CardEffect> onPlayEffects
        )
        {
            Model = model;
            Name = name;
            Text = text;
            Sprite = sprite;
            Type = type;
            Cost = cost;
            BaseMana = baseMana;
            ToAddToMultiplier = toAddToMultiplier;
            ToMultiplyByMultiplier = toMultiplyByMultiplier;
            OnPlayEffects = onPlayEffects;
            UUID = Guid.NewGuid().ToString();
        }

        public bool Equals(Card other)
        {
            return UUID == other.UUID;
        }
    }
}