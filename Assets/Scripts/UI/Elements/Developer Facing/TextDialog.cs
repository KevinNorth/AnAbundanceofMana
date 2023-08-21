using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace KitTraden.AnAbundanceOfMana.UI.Elements.Dev
{
    public class TextDialog : MonoBehaviour
    {
        public string StateJSON = "";
        public TextMeshProUGUI Body;
        public Button CopyButton;
        public Button CloseButton;

        public void OnEnable()
        {
            Body.SetText(StateJSON);
            CloseButton.onClick.AddListener(Close);
            CopyButton.onClick.AddListener(Copy);
        }

        public void OnDisable()
        {
            CloseButton.onClick.RemoveAllListeners();
            CopyButton.onClick.RemoveAllListeners();
        }

        private void Close()
        {
            this.gameObject.SetActive(false);
        }

        private void Copy()
        {
            GUIUtility.systemCopyBuffer = StateJSON;
        }
    }
}