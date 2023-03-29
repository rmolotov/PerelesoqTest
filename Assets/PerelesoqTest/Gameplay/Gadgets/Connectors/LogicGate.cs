using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PerelesoqTest.Gameplay.Gadgets.Connectors
{
    public class LogicGate : ConnectorBase
    {
        private const int On  = Constants.OnCurrentValue;
        private const int Off = Constants.OffCurrentValue;

        [EnumToggleButtons] [SerializeField] 
        private LogicGateType gateType;

        private bool _booleanOutput;

        protected override void ChangeOutputCurrent()
        {
            _booleanOutput = CalculateBooleanOutput();
            outputPort.Current = _booleanOutput 
                ? On
                : Off;
            
            base.ChangeOutputCurrent();
        }
        
        protected override void ReportStatus()
        {
            info.Status = _booleanOutput;
            base.ReportStatus();
        }

        private bool CalculateBooleanOutput() =>
            gateType switch
            {
                LogicGateType.NOT => !(inputPort.Inputs[0].Current > Off),
                LogicGateType.AND =>   inputPort.Inputs[0].Current > Off && inputPort.Inputs[1].Current > Off,
                LogicGateType.OR  =>   inputPort.Inputs[0].Current > Off || inputPort.Inputs[1].Current > Off,
                LogicGateType.XOR =>   inputPort.Inputs[0].Current > Off ^  inputPort.Inputs[1].Current > Off,
                _                 => throw new ArgumentOutOfRangeException()
            };
    }
}