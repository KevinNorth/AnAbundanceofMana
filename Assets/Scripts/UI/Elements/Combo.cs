using UnityEngine;
using KitTraden.AnAbundanceOfMana.Managers;
using KitTraden.AnAbundanceOfMana.MVC.Views;
using KitTraden.AnAbundanceOfMana.Calculators.Score;

namespace KitTraden.AnAbundanceOfMana.UI.Elements
{
    public class Combo : MonoBehaviour
    {
        public GameObject CardPrefab;
        public RectTransform ContentContainer;

        private SpellManager Manager;
        private OverallView View { get => Manager.GetView(); }
        private OverallView PreviousView = null;

        public void OnEnable()
        {
            Manager = FindObjectOfType<SpellManager>();
        }

        public void Update()
        {
            if (View == PreviousView)
            {
                return;
            }
            PreviousView = View;

            ClearPreviousCards();

            var cards = View.cardsView.Combo;
            var scores = View.scoreView.ScoreCalculation.CalculationSteps;
            for (int i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var score = scores[i];
                RenderCard(card, score);
            }
        }

        private void ClearPreviousCards()
        {
            for (int i = ContentContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(ContentContainer.GetChild(i).gameObject);
            }
        }

        private void RenderCard(CardView card, ScoreCalculationStep scoreCalculationStep)
        {
            var newGameObject = Instantiate(CardPrefab);

            var fullCard = newGameObject.GetComponentInChildren<FullCard>();
            fullCard.Card = card;
            fullCard.Draggable = false;

            var scoreCalculationStepDisplay = newGameObject.GetComponentInChildren<ScoreCalculationStepDisplay>();
            scoreCalculationStepDisplay.ScoreCalculationStep = scoreCalculationStep;

            newGameObject.transform.SetParent(ContentContainer.transform);
            newGameObject.transform.localScale = Vector2.one;
        }
    }
}