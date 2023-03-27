using PerelesoqTest.Gameplay.Gadgets.Ports;
using PerelesoqTest.Services.Logging;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace PerelesoqTest.Gameplay.Gadgets.Functions
{
    [RequireComponent(typeof(GadgetBaseInfo))]
    [RequireComponent(typeof(GadgetPowerInfo))]
    [RequireComponent(typeof(InputPort))]
    public abstract class BaseFunction : MonoBehaviour
    {
        [BoxGroup("Info components")] [SerializeField]
        protected GadgetBaseInfo info;
        [BoxGroup("Info components")] [SerializeField]
        protected GadgetPowerInfo power;

        [BoxGroup("Connections")] [SerializeField]
        protected InputPort inputPort;
        
        private ILoggingService _loggingService;
        
        [Inject]
        private void Construct(ILoggingService logger) => 
            _loggingService = logger;

        [Button, GUIColor(0,1,0)]
        public virtual void Activate()
        {
            power.Active = true;
            ReportState(power.Active);
        }

        [Button, GUIColor(1,0,0)]
        public virtual void Deactivate()
        {
            power.Active = false;
            ReportState(power.Active);
        }

        private void ReportState(bool state) =>
            _loggingService.LogMessage(
                state
                    ? "activated"
                    : "deactivated",
                GetType().Name);
    }
}