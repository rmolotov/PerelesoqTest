using PerelesoqTest.Gameplay.Gadgets.Ports;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PerelesoqTest.Gameplay.Gadgets
{
    [RequireComponent(typeof(OutputPort))]
    public class PowerSource : MonoBehaviour
    {
        [BoxGroup("Connections")] [SerializeField]
        private OutputPort outputPort;

        [BoxGroup("Indicators")] [SerializeField]
        private MeshRenderer indicatorMesh;
        [BoxGroup("Indicators")] [SerializeField]
        private Material onMaterial, offMaterial;

        [ShowInInspector, ReadOnly][TitleGroup("Actions/Status", Order = 1)]
        private bool _active;

        private void Start() => 
            InitialPowerOn();

        [BoxGroup("Actions")][ButtonGroup("Actions/Buttons")]
        [Button, GUIColor(0.89f, 0.553f, 0.275f)]
        public void Interact()
        {
            _active = !_active;
            ChangeOutputCurrent();
            ChangeOutputIndicatorMaterial();
        }

        private void InitialPowerOn()
        {
            _active = true;
            ChangeOutputCurrent();
            ChangeOutputIndicatorMaterial();
        }

        private void ChangeOutputCurrent()
        {
            outputPort.Current = _active
                ? Constants.OnCurrentValue
                : Constants.OffCurrentValue;
            outputPort.CurrentChanged?.Invoke();
        }

        private void ChangeOutputIndicatorMaterial() =>
            indicatorMesh.sharedMaterial = outputPort.Current > 0
                ? onMaterial
                : offMaterial;
    }
}