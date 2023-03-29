using PerelesoqTest.Gameplay.Gadgets;
using PerelesoqTest.Gameplay.Gadgets.Connectors;
using UnityEngine;
using UnityEngine.UI;

namespace PerelesoqTest.Gameplay.UI.Widgets
{
    public class WidgetSwitch: WidgetBase
    {
        private const string ActivatedStatus   = "<color=green>ON</color>";
        private const string DeactivatedStatus = "<color=red>OFF</color>";
        
        [SerializeField] private Toggle toggle;
        
        public override void Initialize(GadgetBaseInfo gadgetInfo)
        {
            var toggleSwitch = gadgetInfo.GetComponent<ToggleSwitch>();
            
            toggle.isOn = (bool) gadgetInfo.Status;
            toggle.onValueChanged.AddListener(_ => toggleSwitch.Interact());

            base.Initialize(gadgetInfo);
        }

        protected override void OnGadgetStatusChanged(object status) =>
            statusText.text = (bool)status
                ? ActivatedStatus
                : DeactivatedStatus;
    }
}