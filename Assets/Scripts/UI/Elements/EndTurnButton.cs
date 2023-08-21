using KitTraden.AnAbundanceOfMana.Managers;
using KitTraden.AnAbundanceOfMana.UI.Events;
using UnityEngine;
using UnityEngine.UI;

namespace KitTraden.AnAbundanceOfMana.UI.Elements
{
    public class EndTurnButton : MonoBehaviour
    {
        public SpellManager Manager;

        private Button Button;

        public void OnEnable()
        {
            Button = GetComponent<Button>();
            Button.onClick.AddListener(OnClick);

            if (Manager == null)
            {
                Manager = FindObjectOfType<SpellManager>();
            }
        }

        public void OnDisable()
        {
            Button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            Manager.HandleUIEvent(new EndTurnEvent());
        }
    }
}