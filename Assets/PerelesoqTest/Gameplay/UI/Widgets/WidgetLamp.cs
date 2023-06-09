﻿namespace PerelesoqTest.Gameplay.UI.Widgets
{
    public class WidgetLamp : WidgetBase
    {
        private const string ActivatedStatus   = "<color=yellow>ON</color>";
        private const string DeactivatedStatus = "<color=grey>OFF</color>";
        
        protected override void OnGadgetStatusChanged(object status) =>
            statusText.text = (bool)status
                ? ActivatedStatus
                : DeactivatedStatus;
    }
}