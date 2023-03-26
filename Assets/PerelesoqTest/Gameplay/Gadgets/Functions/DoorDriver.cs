using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PerelesoqTest.Gameplay.Gadgets.Functions
{
    public class DoorDriver : BaseFunction
    {
        private const string OpenText = "<color=green>OPEN</color>";
        private const string ClosedText = "<color=red>CLOSED</color>";
        
        [SerializeField] private TextMeshPro stateText;
        
        [InfoBox("Link door transform on the level scene", InfoMessageType.Warning, "@doorPivot == null")]
        [SerializeField] private Transform doorPivot;

        [BoxGroup("Opening specs")] [PropertyTooltip("Opening duration in seconds")]
        [Range(0.1f, 60f)]
        [SerializeField] private float openingDuration;
        
        [BoxGroup("Opening specs")]
        [Range(1f, 180f)]
        [SerializeField] private float openingAngle;

        private bool _isOpen = false;
        
        public override void Activate()
        {
            AnimateDoor();
            base.Activate();
        }

        private void AnimateDoor() =>
            doorPivot
                .DORotate(
                    Vector3.up * (_isOpen
                        ? 0
                        : openingAngle),
                    openingDuration)
                .SetEase(Ease.OutBounce)
                .onComplete += () =>
                {
                    _isOpen = !_isOpen;
                    ChangeStateText();
                    Deactivate();
                };

        private void ChangeStateText() =>
            stateText.text = _isOpen 
                ? OpenText 
                : ClosedText;
    }
}