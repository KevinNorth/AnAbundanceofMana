using KitTraden.AnAbundanceOfMana.UI.Events;
using KitTraden.AnAbundanceOfMana.UI.Feedback;
using UnityEngine;

namespace KitTraden.AnAbundanceOfMana.Managers
{
    public abstract class Manager<TView> : MonoBehaviour
    {
        public abstract TView GetView();
        public abstract void HandleUIEvent(UIEvent uiEvent);
        public abstract void RestoreState(int index);
    }
}