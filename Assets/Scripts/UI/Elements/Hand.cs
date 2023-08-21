using UnityEngine;
using KitTraden.AnAbundanceOfMana.Managers;
using KitTraden.AnAbundanceOfMana.MVC.Views;
using KitTraden.AnAbundanceOfMana.Calculators.Score;

namespace KitTraden.AnAbundanceOfMana.UI.Elements
{
    public class Hand : MonoBehaviour
    {
        public GameObject CardPrefab;
        public RectTransform ContentContainer;
        public RectTransform ComboLocation;
        public Canvas Canvas;

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

            var cards = View.cardsView.Hand;
            for (int i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                RenderCard(card, i);
            }
        }

        private void ClearPreviousCards()
        {
            for (int i = ContentContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(ContentContainer.GetChild(i).gameObject);
            }
        }

        private void RenderCard(CardView card, int index)
        {
            var newGameObject = Instantiate(CardPrefab);
            var fullCard = newGameObject.GetComponent<FullCard>();
            fullCard.Card = card;
            fullCard.Index = index;
            fullCard.ComboLocation = ComboLocation;
            fullCard.Canvas = Canvas;
            fullCard.Draggable = true;

            newGameObject.transform.SetParent(ContentContainer.transform);
            newGameObject.transform.localScale = Vector2.one;
        }
    }
}