using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.Effects.CardEffects;
using UnityEngine;
using CardModel = KitTraden.AnAbundanceOfMana.MVC.Models.Cards.Card;

namespace KitTraden.AnAbundanceOfMana.MVC.Views
{
    public class CardView
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

        public EnergyCostView Cost;
        public int BaseMana = 0;
        public decimal ToAddToMultiplier = 0;
        public decimal ToMultiplyByMultiplier = 1;

        public List<CardEffect> OnPlayEffects;

        public CardView(CardView cardToCopy)
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
        }

        public CardView(
            CardModel model,
            string name,
            string text,
            Sprite sprite,
            CardType type,
            EnergyCostView cost,
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
        }
    }
}