using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PerelesoqTest.Gameplay.Gadgets
{
    public class ElectricityMeter : MonoBehaviour
    {
        private const int RefreshPeriod = 1;
        
        [BoxGroup("ElectricityMeter"), ReadOnly] [SerializeField] private int _currentPower;
        [BoxGroup("ElectricityMeter"), ReadOnly] [SerializeField] private double _totalPower;
        [BoxGroup("ElectricityMeter"), ReadOnly] [SerializeField] private ulong _uptimeSeconds = 0;

        [BoxGroup("UI Components")] [SerializeField] private TextMeshProUGUI currentText, totalText, timerText;

        [ReadOnly][TitleGroup("Status")]
        public bool Active;
        
        private IEnumerable<GadgetPowerInfo> _gadgets;

        public void Initialize(IEnumerable<GadgetPowerInfo> gadgets)
        {
            _gadgets = gadgets;

            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    UpdateElectricityMeter();
                    UpdateUI();
                })
                .AppendInterval(RefreshPeriod)
                .SetLoops(-1, LoopType.Incremental)
                .Play();
        }

        private void UpdateElectricityMeter()
        {
            _currentPower = Active
                ? CalculateCurrentPower()
                : 0;
            if (!Active) return;
            
            _uptimeSeconds += RefreshPeriod;
            _totalPower += _currentPower / 3600f;
        }

        private void UpdateUI()
        {
            timerText.text =   $"TIME: {_uptimeSeconds/3600}H {_uptimeSeconds % 3600 / 60}M {_uptimeSeconds % 3600 % 60}s";
            currentText.text = $"CURRENT: {_currentPower}W";
            totalText.text =   $"TOTAL: {(int)_totalPower}W";
        }

        private int CalculateCurrentPower()
        {
            // TODO: can be converted into LINQ but becomes inefficient? Better refactor with Rx
            var currentSum = 0;
            foreach (var gadget in _gadgets)
                if (gadget.Active)
                    currentSum += gadget.PowerPerHour;
            return currentSum;
        }
    }
}