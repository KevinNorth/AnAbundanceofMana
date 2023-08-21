using UnityEngine;
using TMPro;
using KitTraden.AnAbundanceOfMana.Managers;
using KitTraden.AnAbundanceOfMana.MVC.Views;
using Michsky.UI.MTP;

namespace KitTraden.AnAbundanceOfMana.UI.Renderers
{
    public class SpellRenderer : MonoBehaviour
    {
        public TextMeshProUGUI SpellNameText;
        public TextMeshProUGUI MulligansText;
        public TextMeshProUGUI EnergyText;
        public TextMeshProUGUI PhaseText;
        public TextMeshProUGUI QuotaText;
        public TextMeshProUGUI ScoreText;
        public TextMeshProUGUI BonusBaseManaText;
        public TextMeshProUGUI BonusMultiplierToAddText;
        public TextMeshProUGUI BonusMultiplierToMultiplyText;
        public StyleManager Toast;

        private SpellManager Manager;
        private OverallView View { get => Manager.GetView(); }

        public void OnEnable()
        {
            Manager = FindObjectOfType<SpellManager>();
        }

        public void Update()
        {
            RenderTexts();
        }

        public void HandleFeedback(Feedback.Feedback feedback)
        {
            if (feedback.ToastText != null)
            {
                foreach (var text in Toast.textItems)
                {
                    text.text = feedback.ToastText;
                }
                Toast.Play();
            }
        }

        private void RenderTexts()
        {
            SpellNameText.SetText(View.spellView.Name);
            PhaseText.SetText($"Phase {View.spellView.PhaseIndex + 1} of {View.spellView.Phases.Count}");
            EnergyText.SetText($"{View.playerView.EnergyRemaining}/{View.playerView.EnergyPerTurn}");
            ScoreText.SetText($"{View.scoreView.ScoreCalculation.FinalScore:#,##0.#} mana");
            QuotaText.SetText($"{View.spellView.Phases[View.spellView.PhaseIndex].ManaQuota}");
            MulligansText.SetText($"{View.playerView.MulligansRemaining} assays remain");
            BonusBaseManaText.SetText($"{View.playerView.CurrentTurnBaseManaBonus:#,##0}");
            BonusMultiplierToAddText.SetText($"{View.playerView.CurrentTurnMultiplierBonusToAdd:#,##0.0}");
            BonusMultiplierToMultiplyText.SetText($"{View.playerView.CurrentTurnMultiplierBonusToMultiply:#,##0.0}");
        }
    }
}