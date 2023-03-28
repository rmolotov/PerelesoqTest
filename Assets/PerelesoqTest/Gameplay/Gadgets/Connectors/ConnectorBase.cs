using System;
using System.Collections.Generic;
using PerelesoqTest.Gameplay.Gadgets.Ports;
using PerelesoqTest.Services.Logging;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace PerelesoqTest.Gameplay.Gadgets.Connectors
{
    [RequireComponent(typeof(InputPort))]
    [RequireComponent(typeof(OutputPort))]
    public abstract class ConnectorBase : MonoBehaviour
    {
        [BoxGroup("Connections")] [SerializeField]
        protected InputPort inputPort;
        [BoxGroup("Connections")] [SerializeField]
        protected OutputPort outputPort;

        [InfoBox("In most cases indicators count should be equal to count of input ports. Please set them up on the level scene!", 
            InfoMessageType.Warning, 
            "@inputIndicatorsMeshes.Count != inputPort.Inputs.Count || inputIndicatorsMeshes.Count == 0")]
        [BoxGroup("Indicators")] [SerializeField]
        protected List<MeshRenderer> inputIndicatorsMeshes;
        [BoxGroup("Indicators")] [SerializeField]
        protected MeshRenderer outputIndicatorMesh;
        [BoxGroup("Indicators")] [SerializeField]
        protected Material onMaterial, offMaterial;

        protected ILoggingService LoggingService;
        private Action<int> _currentHandler;

        [Inject]
        private void Construct(ILoggingService logger) => 
            LoggingService = logger;
        
        protected virtual void OnEnable()
        {
            inputPort.CurrentChanged += _currentHandler = _ => ChangeOutputCurrent();
            inputPort.CurrentChanged += ChangeInputIndicatorMaterial;

            outputPort.CurrentChanged += ChangeOutputIndicatorMaterial;
        }

        protected virtual void OnDisable()
        {
            inputPort.CurrentChanged -= _currentHandler;
            inputPort.CurrentChanged -= ChangeInputIndicatorMaterial;
            
            outputPort.CurrentChanged -= ChangeOutputIndicatorMaterial;
        }

        protected virtual void ChangeOutputCurrent() => 
            outputPort.CurrentChanged?.Invoke();

        private void ChangeInputIndicatorMaterial(int index)
        {
            if (index < inputIndicatorsMeshes.Count)
                inputIndicatorsMeshes[index].sharedMaterial =
                    inputPort.Inputs[index].Current > Constants.OffCurrentValue
                        ? onMaterial
                        : offMaterial;
        }

        private void ChangeOutputIndicatorMaterial() =>
            outputIndicatorMesh.sharedMaterial = outputPort.Current > Constants.OffCurrentValue
                ? onMaterial
                : offMaterial;
    }
}