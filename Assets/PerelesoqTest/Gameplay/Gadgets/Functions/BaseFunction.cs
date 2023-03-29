using PerelesoqTest.Gameplay.Gadgets.Ports;
using PerelesoqTest.Services.Logging;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace PerelesoqTest.Gameplay.Gadgets.Functions
{
    [RequireComponent(typeof(GadgetBaseInfo))]
    [RequireComponent(typeof(InputPort))]
    [RequireComponent(typeof(GadgetPowerInfo))]
    public abstract class BaseFunction : MonoBehaviour
    {
        [BoxGroup("Info components")] [SerializeField]
        protected GadgetBaseInfo info;
        [BoxGroup("Info components")] [SerializeField]
        protected GadgetPowerInfo power;

        [BoxGroup("Connections")] [SerializeField]
        protected InputPort inputPort;
        
        protected ILoggingService _loggingService;
        
        [Inject]
        private void Construct(ILoggingService logger) => 
            _loggingService = logger;

        [BoxGroup("Actions")][ButtonGroup("Actions/Buttons")]
        [Button, GUIColor(0.89f, 0.553f, 0.275f)]
        public virtual void Interact() => 
            _loggingService.LogMessage("react to interaction", GetType().Name);

        [ButtonGroup("Actions/Buttons")]
        [Button, GUIColor(0,1,0)]
        protected virtual void Activate() => 
            power.Active = true;

        [ButtonGroup("Actions/Buttons")]
        [Button, GUIColor(1,0,0)]
        protected virtual void Deactivate() => 
            power.Active = false;

        protected virtual void ReportStatus(bool state)
        {
            info.StatusChanged?.Invoke(info.Status);
            _loggingService.LogMessage(
                state
                    ? "activated"
                    : "deactivated",
                GetType().Name);
        }
    }
}