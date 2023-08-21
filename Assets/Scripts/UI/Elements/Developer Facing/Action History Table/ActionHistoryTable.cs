using UnityEngine;
using KitTraden.AnAbundanceOfMana.Managers;
using KitTraden.AnAbundanceOfMana.MVC.Controllers;

namespace KitTraden.AnAbundanceOfMana.UI.Elements.Dev
{
    public class ActionHistoryTable : MonoBehaviour
    {
        public GameObject RowPrefab;
        public Transform TableBodyTransform;
        public TextDialog StateDialog;

        private SpellManager Manager;
        private Controller Controller;

        public void OnEnable()
        {
            Manager = FindObjectOfType<SpellManager>();
            Controller = Manager.Controller;

            Rerender();
        }

        public void Rerender()
        {
            ClearPreviousTable();

            for (int index = 0; index < Controller.PreviousActions.Count; index++)
            {
                var newRow = Instantiate(RowPrefab, TableBodyTransform);
                var newRowScript = newRow.GetComponent<ActionHistoryTableRow>();
                newRowScript.index = index;
                newRowScript.StateDialog = StateDialog;
                newRowScript.ParentTable = this;
                newRowScript.Rerender();
            }
        }

        private void ClearPreviousTable()
        {
            for (int i = TableBodyTransform.childCount - 1; i >= 0; i--)
            {
                Destroy(TableBodyTransform.GetChild(i).gameObject);
            }
        }
    }
}