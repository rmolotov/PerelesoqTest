﻿using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PerelesoqTest.Gameplay.Gadgets.Connectors
{
    public class ToggleSwitch : ConnectorBase
    {
        [BoxGroup("Switching specs")] [SerializeField]
        private Transform togglePivot;
        [BoxGroup("Switching specs")] [PropertyTooltip("Opening duration in seconds")] [Range(0.1f, 60f)] [SerializeField]
        private float switchingDuration;
        [BoxGroup("Switching specs")] [Range(0.1f, 180f)] [SerializeField]
        private float switchingAngle;
        
        [ShowInInspector][TitleGroup("Actions/Status", Order = 1)][SerializeField]
        private bool active;
        
        [BoxGroup("Actions")][ButtonGroup("Actions/Buttons")]
        [Button, GUIColor(0.89f, 0.553f, 0.275f)]
        public void Interact()
        {
            AnimateSwitch();
            LoggingService.LogMessage("react to interaction", this);
        }

        protected override void ChangeOutputCurrent()
        {
            outputPort.Current = active 
                ? inputPort.Inputs[0].Current
                : Constants.OffCurrentValue;
            base.ChangeOutputCurrent();
        }

        protected override void ReportStatus()
        {
            info.Status = active;
            base.ReportStatus();
        }

        private void AnimateSwitch() =>
            togglePivot
                .DOLocalRotate(
                    Vector3.forward * (active
                        ? switchingAngle
                        : -switchingAngle),
                    switchingDuration)
                .SetEase(Ease.InOutExpo)
                .onComplete += () =>
                {
                    active = !active;
                    ChangeOutputCurrent();
                };
    }
}