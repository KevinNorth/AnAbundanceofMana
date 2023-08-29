using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using KitTraden.AnAbundanceOfMana.Effects.CardEffects;
using KitTraden.AnAbundanceOfMana.State.Entities;
using CardModel = KitTraden.AnAbundanceOfMana.MVC.Models.Cards.Card;

namespace KitTraden.AnAbundanceOfMana.Tests.Factories.State.Entities
{
    public class CardFactory
    {
        private static int NUM_CARDS_CREATED = 0;

        public static Card CreateCard(
            CardModel model = null,
            string name = null,
            string text = null,
            Sprite sprite = null,
            Card.CardType type = Card.CardType.COMBOABLE,
            EnergyCost cost = null,
            int baseMana = 1,
            decimal toAddToMultiplier = 0,
            decimal toMultiplyByMultiplier = 1,
            List<CardEffect> onPlayEffects = null,
            string uuid = null)
        {
            var card = new Card(model, name, text, sprite, type, cost, baseMana, toAddToMultiplier, toMultiplyByMultiplier, onPlayEffects);

            if (name == null)
            {
                card.Name = $"Test Card {NUM_CARDS_CREATED:0}";
            }

            if (text == null)
            {
                card.Text = $"Test text {NUM_CARDS_CREATED:0}";
            }

            if (uuid != null)
            {
                card.UUID = uuid;
            }

            NUM_CARDS_CREATED++;

            return card;
        }

        public static Card CopyCard(Card card)
        {
            return new Card(card, copyGUID: true);
        }

        public static List<Card> CopyCards(List<Card> cards)
        {
            return cards.Select((card) => CopyCard(card)).ToList();
        }
    }
}