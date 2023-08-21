using UnityEngine;
using UnityEngine.UI;

namespace KitTraden.AnAbundanceOfMana.UI.Elements.Dev
{
    public class DeveloperUI : MonoBehaviour
    {
        public GameObject StateTable;
        public Button CloseButton;

        public void OnEnable()
        {
            CloseButton.onClick.AddListener(Close);
        }

        public void OnDisable()
        {
            CloseButton.onClick.RemoveAllListeners();
        }

        public void ShowStateTable()
        {
            gameObject.SetActive(true);
            StateTable.SetActive(true);
        }

        public void Close()
        {
            StateTable.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}