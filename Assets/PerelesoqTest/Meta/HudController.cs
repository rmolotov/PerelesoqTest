using PerelesoqTest.Gameplay.Gadgets;
using PerelesoqTest.Infrastructure.States;
using PerelesoqTest.Services.Logging;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PerelesoqTest.Meta
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Button resetButton;
        [BoxGroup("ElectricityMeter")] [SerializeField] private TextMeshProUGUI currentText, totalText, timerText;
        
        public RectTransform WidgetsContainer;
        
        private GameStateMachine _stateMachine;
        private ILoggingService _logger;

        [Inject]
        private void Construct(GameStateMachine stateMachine, ILoggingService loggingService)
        {
            _stateMachine = stateMachine;
            _logger = loggingService;
        }

        public void Initialize(ElectricityMeter electricityMeter)
        {
            SetupButtons();
            SetupPowerSourceDisplay(electricityMeter);
            
            _logger.LogMessage("initialized", this);
        }

        private void SetupButtons() =>
            resetButton.onClick.AddListener(() => 
                _stateMachine.Enter<LoadMetaState>());

        private void SetupPowerSourceDisplay(ElectricityMeter electricityMeter)
        {
            electricityMeter.ValuesUpdated += UpdateUI;
        }
        
        private void UpdateUI(int current, float total, ulong upTime)
        {
            var hours   = upTime / 3600;
            var minutes = upTime % 3600 / 60;
            var seconds = upTime % 3600 % 60;
            
            timerText.text   = $"TIME: {hours:00}H {minutes:00}M {seconds:00}S";
            currentText.text = $"CURRENT: {current:000}W";
            totalText.text   = $"TOTAL: {(int)total:00000}W";
        }
    }
}