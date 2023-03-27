using PerelesoqTest.Gameplay.Gadgets.Ports;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PerelesoqTest.Gameplay.Gadgets
{
    [RequireComponent(typeof(OutputPort))]
    public class PowerSource : MonoBehaviour
    {
        private const int OnCurrentValue  = 220;
        private const int OffCurrentValue = 0;

        [BoxGroup("Connections")] [SerializeField]
        private OutputPort outputPort;

        [BoxGroup("Indicators")] [SerializeField]
        private MeshRenderer indicatorMesh;
        [BoxGroup("Indicators")] [SerializeField]
        private Material onMaterial, offMaterial;

        [ShowInInspector, ReadOnly]
        private bool _active = false;
        
        
        [Button, GUIColor(0.89f, 0.553f, 0.275f)]
        private void Interact()
        {
            _active = !_active;
            ChangeOutputCurrent();
            ChangeIndicatorMaterial();
        }
        
        private void ChangeOutputCurrent()
        {
            outputPort.Current = _active
                ? OnCurrentValue
                : OffCurrentValue;
            outputPort.CurrentChanged?.Invoke();
        }

        private void ChangeIndicatorMaterial() =>
            indicatorMesh.sharedMaterial = outputPort.Current > 0
                ? onMaterial
                : offMaterial;
    }
}