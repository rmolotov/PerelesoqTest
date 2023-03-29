using UnityEngine;
using TMPro;
using PerelesoqTest.Gameplay.Gadgets;

namespace PerelesoqTest.Gameplay.UI.Widgets
{
    public class WidgetBase : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI nameText;
        [SerializeField] protected TextMeshProUGUI statusText;
        
        public void Initialize(GadgetBaseInfo gadgetInfo)
        {
            nameText.text = gadgetInfo.DisplayName;
            statusText.text = gadgetInfo.Status;
        }
    }
}