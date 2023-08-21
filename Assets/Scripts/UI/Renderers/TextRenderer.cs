using System.Linq;
using UnityEngine;
using TMPro;
using KitTraden.AnAbundanceOfMana.Managers;
using KitTraden.AnAbundanceOfMana.MVC.Views;
using System.Collections.Generic;

namespace KitTraden.AnAbundanceOfMana.UI.Renderers
{
    public class TextRenderer : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public SpellManager manager;

        public void Update()
        {
            var view = manager.GetView();

            var str = "*** Player: ***\n";
            str += $"Energy: {view.playerView.EnergyRemaining}/{view.playerView.EnergyPerTurn}\n";
            str += $"Mulligans: {view.playerView.MulligansRemaining}/{view.playerView.MaxMulligans}\n";
            str += $"Cards drawn each turn: {view.playerView.StartOfTurnHandSize}\n";
            str += $"Max hand size: {view.playerView.MaxHandSize}\n";
            str += "\n";
            str += "*** Spell: ***\n";
            str += $"Name: {view.spellView.Name}\n";
            str += $"Current phase: {view.spellView.PhaseIndex + 1}\n";
            str += $"Phases: {string.Join(", ", view.spellView.Phases.Select((phase, index) => $"{index + 1}: {phase.ManaQuota} ({phase.MulliganBehavior})"))}\n";
            str += "\n";
            str += "*** Cards: ***\n";
            str += $"Permanent deck: {ListOfCardsToString(view.cardsView.PermamentDeck)}\n";
            str += $"Deck: {ListOfCardsToString(view.cardsView.Deck)}\n";
            str += $"Hand: {ListOfCardsToString(view.cardsView.Hand)}\n";
            str += $"Combo: {ListOfCardsToString(view.cardsView.Combo)}\n";
            str += $"Discard pile: {ListOfCardsToString(view.cardsView.DiscardPile)}\n";
            str += $"Exhaust pile: {ListOfCardsToString(view.cardsView.ExhaustPile)}\n";
            str += $"Spent powers: {ListOfCardsToString(view.cardsView.SpentPowerPile)}";

            text.SetText(str);
        }

        private string ListOfCardsToString(List<CardView> cards)
        {
            var cardStrings = cards.Select((card, _index) => CardToString(card));
            return string.Join(", ", cardStrings);
        }

        private string CardToString(CardView card)
        {
            var costString = card.Cost.Type == EnergyCostView.CostType.X ? "X" : card.Cost.ConstantCost.ToString();

            return $"{card.Name} ({costString}) (+{card.BaseMana} x{card.ToAddToMultiplier} ^{card.ToMultiplyByMultiplier})";
        }
    }
}