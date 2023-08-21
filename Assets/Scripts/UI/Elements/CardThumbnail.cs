using UnityEngine;
using TMPro;
using KitTraden.AnAbundanceOfMana.MVC.Views;
using UnityEngine.UI;

namespace KitTraden.AnAbundanceOfMana.UI.Elements
{
    public class CardThumbnail : MonoBehaviour
    {
        public CardView Card;
        public TextMeshProUGUI Name;
        public Image Image;
        public LayoutElement LayoutElement;
        public float Height;
        public float Width;

        public void Update()
        {
            Name.SetText(Card?.Name);
            Image.rectTransform.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical,
                Image.rectTransform.rect.height
            );
            Image.sprite = Card?.Sprite;

            LayoutElement.minWidth = Width;
            LayoutElement.preferredHeight = Height;
        }
    }
}