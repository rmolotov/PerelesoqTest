using PerelesoqTest.Gameplay.Gadgets;
using PerelesoqTest.Gameplay.Gadgets.Functions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PerelesoqTest.Gameplay.UI.Widgets
{
    public class WidgetDoorDriver : WidgetBase
    {
        private const string ActivatedStatus   = "OPEN";
        private const string DeactivatedStatus = "CLOSE";
        
        [SerializeField] private Button interactButton;
        [SerializeField] private TextMeshProUGUI buttonText;
        public override void Initialize(GadgetBaseInfo gadgetInfo)
        {
            var doorDriver = gadgetInfo.GetComponent<DoorDriver>();
            
            interactButton.onClick.AddListener(() =>
            {
                interactButton.interactable = false;
                doorDriver.Interact();
            });
            
            base.Initialize(gadgetInfo);
        }
        
        protected override void OnGadgetStatusChanged(object status)
        {
            var value = status is bool and true;
            
            SetStatusText(value);
            SetButtonText(value);
            interactButton.interactable = true;
        }

        private void SetStatusText(bool status) =>
            statusText.text = status
                ? $"<color=green>{ActivatedStatus}</color>"
                : $"<color=red>{DeactivatedStatus}</color>";

        private void SetButtonText(bool status) =>
            buttonText.text = status
                ? DeactivatedStatus
                : ActivatedStatus;
    }
}