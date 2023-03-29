using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace PerelesoqTest.Gameplay.Gadgets.Functions
{
    public class CleanerBot : BaseFunction
    {
        private const float CleaningTime = 10;

        [InfoBox("In most cases indicators count should be equal to count of input ports. Please set them up on the level scene!", 
            InfoMessageType.Warning, 
            "@inputIndicatorsMeshes.Count != inputPort.Inputs.Count || inputIndicatorsMeshes.Count == 0")]
        [BoxGroup("Indicators")] [SerializeField]
        protected List<MeshRenderer> inputIndicatorsMeshes;

        [BoxGroup("Indicators")] [SerializeField]
        protected Material onMaterial, offMaterial;

        [SerializeField] private Transform botTransform;

        private void OnEnable() => 
            inputPort.CurrentChanged += ChangeInputIndicatorMaterial;

        private void OnDisable() => 
            inputPort.CurrentChanged -= ChangeInputIndicatorMaterial;

        public override void Interact()
        {
            if (inputPort.Inputs[0].Current == Constants.OffCurrentValue)
                return;
            
            RunBot();
        }

        protected override void ReportStatus(bool state)
        {
            info.Status = state;
            base.ReportStatus(state);
        }

        private void RunBot() =>
            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    _loggingService.LogMessage("bzzzz", GetType().Name);
                    Activate();
                    botTransform
                        .DOLocalMove(2 * Vector3.back, CleaningTime / 2);
                    ReportStatus(power.Active);
                })
                .AppendInterval(CleaningTime / 2)
                .AppendCallback(() =>
                {
                    _loggingService.LogMessage("bzzzz", GetType().Name);
                    botTransform
                        .DOLocalMove(Vector3.zero, CleaningTime / 2);
                })
                .AppendInterval(CleaningTime / 2)
                .AppendCallback(() =>
                {
                    Deactivate();
                    ReportStatus(power.Active);
                })
                .Play();

        private void ChangeInputIndicatorMaterial(int index)
        {
            if (index < inputIndicatorsMeshes.Count)
                inputIndicatorsMeshes[index].sharedMaterial =
                    inputPort.Inputs[index].Current > Constants.OffCurrentValue
                        ? onMaterial
                        : offMaterial;
        }
    }
}