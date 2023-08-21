using UnityEngine;
using UnityEngine.UI;

namespace KitTraden.AnAbundanceOfMana.UI.Elements.Dev
{
    public class StateHistoryButton : MonoBehaviour
    {
        public DeveloperUI DeveloperUI;

        private Button Button;

        public void OnEnable()
        {
            Button = GetComponent<Button>();
            Button.onClick.AddListener(ShowStateHistoryTable);
        }

        public void OnDisable()
        {
            Button.onClick.RemoveAllListeners();
        }

        public void ShowStateHistoryTable()
        {
            DeveloperUI.ShowStateTable();
        }
    }
}