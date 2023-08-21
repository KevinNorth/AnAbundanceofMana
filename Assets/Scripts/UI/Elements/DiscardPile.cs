using UnityEngine;
using KitTraden.AnAbundanceOfMana.Managers;
using KitTraden.AnAbundanceOfMana.MVC.Views;

namespace KitTraden.AnAbundanceOfMana.UI.Elements
{
    public class DiscardPile : MonoBehaviour
    {
        public GameObject CardThumbnailPrefab;
        public RectTransform ContentContainer;
        public int CardHeight = 40;

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

            var cards = View.cardsView.DiscardPile;
            foreach (var card in cards)
            {
                RenderCard(card);
            }
        }

        private void ClearPreviousCards()
        {
            for (int i = ContentContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(ContentContainer.GetChild(i).gameObject);
            }
        }

        private void RenderCard(CardView card)
        {
            var newGameObject = Instantiate(CardThumbnailPrefab);
            var cardThumbnail = newGameObject.GetComponent<CardThumbnail>();
            cardThumbnail.Card = card;

            var cardWidth = GetComponent<RectTransform>().rect.width;

            var rect = newGameObject.GetComponent<RectTransform>();
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, CardHeight);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cardWidth);
            cardThumbnail.Height = CardHeight;
            cardThumbnail.Width = cardWidth;

            newGameObject.transform.SetParent(ContentContainer.transform);
            newGameObject.transform.localScale = Vector2.one;
        }
    }
}