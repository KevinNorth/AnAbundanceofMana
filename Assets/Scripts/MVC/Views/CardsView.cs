using System.Linq;
using System.Collections.Generic;
using ModelCard = KitTraden.AnAbundanceOfMana.MVC.Models.Cards.Card;
using KitTraden.AnAbundanceOfMana.Calculators.Score;

namespace KitTraden.AnAbundanceOfMana.MVC.Views
{
    public class CardsView
    {
        public enum CardType
        {

        }

        public List<CardView> PermamentDeck;
        public List<CardView> Deck;
        public List<CardView> Hand;
        public List<CardView> Combo;
        public List<CardView> DiscardPile;
        public List<CardView> ExhaustPile;
        public List<CardView> SpentPowerPile;

        private static List<CardView> DeepCopyCards(List<CardView> cardsToDeepCopy)
        {
            return cardsToDeepCopy.Select((cardToCopy, _index) => new CardView(cardToCopy)).ToList();
        }

        public CardsView(
            List<CardView> permanentDeck,
            List<CardView> deck,
            List<CardView> hand,
            List<CardView> combo,
            List<CardView> discardPile,
            List<CardView> exhaustPile,
            List<CardView> spentPowerPile
        )
        {
            this.PermamentDeck = permanentDeck;
            this.Deck = deck;
            this.Hand = hand;
            this.Combo = combo;
            this.DiscardPile = discardPile;
            this.ExhaustPile = exhaustPile;
            this.SpentPowerPile = spentPowerPile;
        }

        public CardsView(CardsView playerStateToCopy)
        {
            this.PermamentDeck = new List<CardView>(playerStateToCopy.PermamentDeck);
            this.Deck = new List<CardView>(playerStateToCopy.Deck);
            this.Hand = new List<CardView>(playerStateToCopy.Hand);
            this.Combo = new List<CardView>(playerStateToCopy.Combo);
            this.DiscardPile = new List<CardView>(playerStateToCopy.DiscardPile);
            this.ExhaustPile = new List<CardView>(playerStateToCopy.ExhaustPile);
            this.SpentPowerPile = new List<CardView>(playerStateToCopy.SpentPowerPile);
        }

        public CardsView DeepCopy()
        {
            var deepCopy = new CardsView(
                DeepCopyCards(PermamentDeck),
                DeepCopyCards(Deck),
                DeepCopyCards(Hand),
                DeepCopyCards(Combo),
                DeepCopyCards(DiscardPile),
                DeepCopyCards(ExhaustPile),
                DeepCopyCards(SpentPowerPile)
            );

            return deepCopy;
        }
    }
}