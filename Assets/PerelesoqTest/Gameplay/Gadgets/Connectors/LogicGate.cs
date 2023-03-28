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

        protected override void ChangeOutputCurrent()
        {
            var booleanOutput = CalculateBooleanOutput();
            outputPort.Current = booleanOutput 
                ? On
                : Off;
            
            base.ChangeOutputCurrent();
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