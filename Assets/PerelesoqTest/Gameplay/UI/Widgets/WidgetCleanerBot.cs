using PerelesoqTest.Gameplay.Gadgets;
using PerelesoqTest.Gameplay.Gadgets.Functions;
using UnityEngine;
using UnityEngine.UI;

namespace PerelesoqTest.Gameplay.UI.Widgets
{
    public class WidgetCleanerBot : WidgetBase
    {
        private const string ActivatedStatus   = "<color=yellow>WORKING</color>";
        private const string DeactivatedStatus = "<color=grey>IDLE</color>";
        
        [SerializeField] private Button interactButton;
        public override void Initialize(GadgetBaseInfo gadgetInfo)
        {
            var cleanerBot = gadgetInfo.GetComponent<CleanerBot>();
            
            interactButton.onClick.AddListener(() => cleanerBot.Interact());
            
            base.Initialize(gadgetInfo);
        }
        
        protected override void OnGadgetStatusChanged(object status)
        {
            var value = status is bool and true;
            
            statusText.text = value
                ? ActivatedStatus
                : DeactivatedStatus;
            
            interactButton.interactable = !value;
        }
    }
}