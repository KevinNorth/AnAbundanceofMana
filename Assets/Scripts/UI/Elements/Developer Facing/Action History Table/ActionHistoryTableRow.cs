using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KitTraden.AnAbundanceOfMana.Managers;
using KitTraden.AnAbundanceOfMana.MVC.Controllers;
using KitTraden.AnAbundanceOfMana.State.Actions;
using KitTraden.AnAbundanceOfMana.State.State;

namespace KitTraden.AnAbundanceOfMana.UI.Elements.Dev
{
    public class ActionHistoryTableRow : MonoBehaviour
    {
        public int index;
        public Action Action { get => Controller.PreviousActions[index]; }
        public OverallState State { get => Controller.PreviousStates[index]; }
        public ActionHistoryTable ParentTable;

        public TextMeshProUGUI IndexText;
        public TextMeshProUGUI TypeText;
        public TextMeshProUGUI ActionJSONText;
        public Button ViewStateButton;
        public Button RestoreStateButton;
        public TextDialog StateDialog;

        private SpellManager Manager;
        private Controller Controller;

        public void OnEnable()
        {
            Manager = FindObjectOfType<SpellManager>();
            Controller = Manager.Controller;

            Rerender();

            ViewStateButton.onClick.AddListener(OnViewStateClicked);
            RestoreStateButton.onClick.AddListener(OnRestoreStateClicked);
        }

        public void OnDisable()
        {
            ViewStateButton.onClick.RemoveAllListeners();
            RestoreStateButton.onClick.RemoveAllListeners();
        }

        public void Rerender()
        {
            IndexText.SetText($"{index:0}");
            TypeText.SetText(Action.GetType().Name);
            ActionJSONText.SetText(JsonUtility.ToJson(Action));
        }

        private void OnViewStateClicked()
        {
            StateDialog.StateJSON = JsonUtility.ToJson(State, true);
            StateDialog.gameObject.SetActive(true);
        }

        private void OnRestoreStateClicked()
        {
            Manager.RestoreState(index);
            ParentTable.Rerender();
        }
    }
}