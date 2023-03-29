using System;
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
        [BoxGroup("ElectricityMeter"), ReadOnly] [SerializeField] private float _totalPower;
        [BoxGroup("ElectricityMeter"), ReadOnly] [SerializeField] private ulong _uptimeSeconds = 0;

        [BoxGroup("UI Components")] [SerializeField] private TextMeshProUGUI currentText, totalText, timerText;

        [ReadOnly][TitleGroup("Status")]
        public bool Active;

        public Action<int, float, ulong> ValuesUpdated;

        private IEnumerable<GadgetPowerInfo> _gadgets;

        public void Initialize(IEnumerable<GadgetPowerInfo> gadgets)
        {
            _gadgets = gadgets;
            ValuesUpdated += UpdateUI;

            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    UpdateElectricityMeter();
                    ValuesUpdated?.Invoke(_currentPower, _totalPower, _uptimeSeconds);
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

        private void UpdateUI(int current, float total, ulong upTime)
        {
            var hours   = upTime / 3600;
            var minutes = upTime % 3600 / 60;
            var seconds = upTime % 3600 % 60;
            
            timerText.text   = $"TIME: {hours:00}H {minutes:00}M {seconds:00}S";
            currentText.text = $"CURRENT: {current:000}W";
            totalText.text   = $"TOTAL: {(int)total:00000}W";
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