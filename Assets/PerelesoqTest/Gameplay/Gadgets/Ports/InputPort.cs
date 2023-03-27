using System;
using System.Collections.Generic;
using UnityEngine;

namespace PerelesoqTest.Gameplay.Gadgets.Ports
{
    public class InputPort : MonoBehaviour
    {
        public List<OutputPort> Inputs;
        public Action<int> CurrentChanged;

        private readonly List<Action> _inputHandlers = new();

        private void OnEnable()
        {
            foreach (var connectedOutput in Inputs)
            {
                void Handler() => CurrentChanged?.Invoke(Inputs.IndexOf(connectedOutput));
                _inputHandlers.Add(Handler);

                connectedOutput.CurrentChanged += Handler;
            }
        }

        private void OnDisable()
        {
            for (var index = 0; index < _inputHandlers.Count; index++)
                Inputs[index].CurrentChanged -= _inputHandlers[index];
            
            _inputHandlers.Clear();
        }
    }
}