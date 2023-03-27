using DG.Tweening;
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
        
        [SerializeField]
        private bool active;
        
        [Button, GUIColor(0.89f, 0.553f, 0.275f)]
        public void Interact()
        {
            AnimateSwitch();
            _loggingService.LogMessage("react to interaction", GetType().Name);
        }

        protected override void ChangeOutputCurrent()
        {
            outputPort.Current = active 
                ? inputPort.Inputs[0].Current
                : 0;
            base.ChangeOutputCurrent();
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