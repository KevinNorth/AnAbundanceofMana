using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KitTraden.AnAbundanceOfMana.MVC.Views;
using UnityEngine.EventSystems;
using KitTraden.AnAbundanceOfMana.Managers;
using KitTraden.AnAbundanceOfMana.UI.Events;
using System;

namespace KitTraden.AnAbundanceOfMana.UI.Elements
{
    public class FullCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public CardView Card;
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Text;
        public TextMeshProUGUI EnergyCost;
        public TextMeshProUGUI BaseMana;
        public TextMeshProUGUI ToAddToMultiplier;
        public TextMeshProUGUI ToMultiplyWithMultiplier;
        public Image Image;
        public int Index;
        public bool Draggable = false;
        public Canvas Canvas;
        public RectTransform ComboLocation;

        private SpellManager Manager;
        private Transform PreviousParent;
        private Vector3 PreviousPosition;
        private RectTransform RectTransform;
        private RectTransform CanvasRectTransform;
        private Vector2 PreviousAnchorMin;
        private Vector2 PreviousAnchorMax;

        private static Vector2 DraggingAnchorMin = new(0.5f, 0.5f);
        private static Vector2 DraggingAnchorMax = new(0.5f, 0.5f);

        public void OnEnable()
        {
            Manager = FindObjectOfType<SpellManager>();
            PreviousParent = transform.parent;
            RectTransform = GetComponent<RectTransform>();
        }

        public void Update()
        {
            Name.SetText(Card?.Name ?? "");
            Text.SetText(Card?.Text ?? "");
            EnergyCost.SetText(GetEnergyCostString());
            BaseMana.SetText(Card?.BaseMana.ToString() ?? "n/a");
            ToAddToMultiplier.SetText(Card?.ToAddToMultiplier.ToString() ?? "n/a");
            ToMultiplyWithMultiplier.SetText(Card?.ToMultiplyByMultiplier.ToString() ?? "n/a");

            Image.sprite = Card?.Sprite;

            PreviousPosition = transform.position;
        }

        private string GetEnergyCostString()
        {
            return Card?.Cost.Type switch
            {
                EnergyCostView.CostType.CONSTANT => Card.Cost.ConstantCost.ToString(),
                EnergyCostView.CostType.X => "X",
                _ => "n/a"
            };
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!Draggable) return;

            CanvasRectTransform = Canvas.GetComponent<RectTransform>();

            PreviousParent = transform.parent;
            PreviousPosition = RectTransform.anchoredPosition;
            transform.SetParent(Canvas.transform, false);

            PreviousAnchorMin = RectTransform.anchorMin;
            PreviousAnchorMax = RectTransform.anchorMax;

            RectTransform.anchorMin = DraggingAnchorMin;
            RectTransform.anchorMax = DraggingAnchorMax;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!Draggable) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                CanvasRectTransform,
                eventData.position,
                Canvas.worldCamera,
                out Vector2 newPosition
            );
            RectTransform.anchoredPosition = newPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!Draggable) return;

            if (RectTransformUtility.RectangleContainsScreenPoint(
                ComboLocation,
                eventData.position,
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>()
            ))
            {
                Manager.HandleUIEvent(new PlayCardEvent(Index));
            }

            transform.SetParent(PreviousParent, false);
            RectTransform.anchoredPosition = PreviousPosition;
            RectTransform.anchorMin = PreviousAnchorMin;
            RectTransform.anchorMax = PreviousAnchorMax;
        }
    }
}