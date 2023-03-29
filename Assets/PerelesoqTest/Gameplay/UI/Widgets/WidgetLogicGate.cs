namespace PerelesoqTest.Gameplay.UI.Widgets
{
    public class WidgetLogicGate : WidgetBase
    {
        private const string ActivatedStatus   = "<color=green>OPENED</color>";
        private const string DeactivatedStatus = "<color=red>CLOSED</color>";
        
        protected override void OnGadgetStatusChanged(object status) =>
            statusText.text = (bool)status
                ? ActivatedStatus
                : DeactivatedStatus;
    }
}