using PerelesoqTest.Gameplay.Gadgets;
using PerelesoqTest.Infrastructure.States;
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

        [Inject]
        private void Construct(GameStateMachine stateMachine) => 
            _stateMachine = stateMachine;
        
        public void Initialize(ElectricityMeter electricityMeter)
        {
            SetupButtons();
            SetupPowerSourceDisplay(electricityMeter);
        }

        private void SetupButtons() =>
            resetButton.onClick.AddListener(() => 
                _stateMachine.Enter<LoadMetaState>());

        private void SetupPowerSourceDisplay(ElectricityMeter electricityMeter)
        {
            
        }
    }
}