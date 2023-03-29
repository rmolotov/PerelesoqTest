namespace PerelesoqTest.Gameplay.UI.Widgets
{
    public class WidgetLamp : WidgetBase
    {
        private const string ActivatedStatus   = "<color=green>ON</color>";
        private const string DeactivatedStatus = "<color=red>OFF</color>";
        
        protected override void OnGadgetStatusChanged(object status) =>
            statusText.text = (bool)status
                ? ActivatedStatus
                : DeactivatedStatus;
    }
}