using UnityEngine;
using TMPro;
using PerelesoqTest.Gameplay.Gadgets;

namespace PerelesoqTest.Gameplay.UI.Widgets
{
    public abstract class WidgetBase : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI nameText;
        [SerializeField] protected TextMeshProUGUI statusText;
        
        public virtual void Initialize(GadgetBaseInfo gadgetInfo)
        {
            nameText.text = $"{gadgetInfo.DisplayName} <color=grey>[{gadgetInfo.Id}]</color>";

            gadgetInfo.StatusChanged += OnGadgetStatusChanged;
            OnGadgetStatusChanged(gadgetInfo.Status);
        }
        
        protected virtual void OnGadgetStatusChanged(object status) =>
            statusText.text = status?.ToString();
    }
}