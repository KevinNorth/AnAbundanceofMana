using KitTraden.AnAbundanceOfMana.Calculators.Score;
using TMPro;
using UnityEngine;

namespace KitTraden.AnAbundanceOfMana.UI.Elements
{
    public class ScoreCalculationStepDisplay : MonoBehaviour
    {
        public TextMeshProUGUI TotalText;
        public TextMeshProUGUI BaseManaText;
        public TextMeshProUGUI ToAddToMultiplierText;
        public TextMeshProUGUI ToMultiplyByMultiplierText;
        public ScoreCalculationStep ScoreCalculationStep;

        public void Update()
        {
            TotalText.SetText($"{ScoreCalculationStep.CurrentScore:#,##0.#}");
            BaseManaText.SetText($"{ScoreCalculationStep.PreviousBaseMana + ScoreCalculationStep.ToAddToBaseMana:#,##0.#}");
            ToAddToMultiplierText.SetText($"{ScoreCalculationStep.PreviousMultiplier + ScoreCalculationStep.ToAddToMultiplier:#,##0.##}");
            ToMultiplyByMultiplierText.SetText($"{ScoreCalculationStep.ToMultiplyByMultiplier:#,##0.##}");
        }
    }
}