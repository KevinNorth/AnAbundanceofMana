using System.Linq;
using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.State.Entities;
using ModelCard = KitTraden.AnAbundanceOfMana.MVC.Models.Cards.Card;
using System;

namespace KitTraden.AnAbundanceOfMana.State.State
{
    [Serializable]
    public class CardsState
    {
        public List<Card> PermamentDeck;
        public List<Card> Deck;
        public List<Card> Hand;
        public List<Card> Combo;
        public List<Card> DiscardPile;
        public List<Card> ExhaustPile;
        public List<Card> SpentPowerPile;

        private static List<Card> deepCopyCards(List<Card> cardsToDeepCopy)
        {
            return cardsToDeepCopy.Select((cardToCopy, _index) => new Card(cardToCopy)).ToList();
        }

        private static List<Card> deepCopyCards(List<ModelCard> cardsToDeepCopy)
        {
            return cardsToDeepCopy.Select((cardToCopy, _index) => new Card(cardToCopy)).ToList();
        }

        public CardsState(List<Card> permanentDeck)
        {
            this.PermamentDeck = deepCopyCards(permanentDeck);
            this.Deck = deepCopyCards(permanentDeck);
            this.Hand = new List<Card>();
            this.Combo = new List<Card>();
            this.DiscardPile = new List<Card>();
            this.ExhaustPile = new List<Card>();
            this.SpentPowerPile = new List<Card>();
        }
        public CardsState(List<ModelCard> permanentDeck)
        {
            this.PermamentDeck = deepCopyCards(permanentDeck);
            this.Deck = deepCopyCards(permanentDeck);
            this.Hand = new List<Card>();
            this.Combo = new List<Card>();
            this.DiscardPile = new List<Card>();
            this.ExhaustPile = new List<Card>();
            this.SpentPowerPile = new List<Card>();
        }

        public CardsState(CardsState playerStateToCopy)
        {
            this.PermamentDeck = new List<Card>(playerStateToCopy.PermamentDeck);
            this.Deck = new List<Card>(playerStateToCopy.Deck);
            this.Hand = new List<Card>(playerStateToCopy.Hand);
            this.Combo = new List<Card>(playerStateToCopy.Combo);
            this.DiscardPile = new List<Card>(playerStateToCopy.DiscardPile);
            this.ExhaustPile = new List<Card>(playerStateToCopy.ExhaustPile);
            this.SpentPowerPile = new List<Card>(playerStateToCopy.SpentPowerPile);
        }

        public CardsState DeepCopy()
        {
            var deepCopy = new CardsState(PermamentDeck);
            deepCopy.Deck = deepCopyCards(Deck);
            deepCopy.Hand = deepCopyCards(Hand);
            deepCopy.Combo = deepCopyCards(Combo);
            deepCopy.DiscardPile = deepCopyCards(DiscardPile);
            deepCopy.ExhaustPile = deepCopyCards(ExhaustPile);
            deepCopy.SpentPowerPile = deepCopyCards(SpentPowerPile);

            return deepCopy;
        }
    }
}