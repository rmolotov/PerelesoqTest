using System;
using System.Collections.Generic;
using UnityEngine;

namespace PerelesoqTest.Gameplay.Gadgets.Ports
{
    public class InputPort : MonoBehaviour
    {
        public List<OutputPort> Inputs;
        public event Action<int> CurrentChanged;

        private readonly List<Action> _inputHandlers = new();

        private void Awake()
        {
            foreach (var input in Inputs)
            {
                void Handler() => CurrentChanged?.Invoke(Inputs.IndexOf(input));
                _inputHandlers.Add(Handler);

                input.CurrentChanged += Handler;
            }
        }

        private void OnDestroy()
        {
            for (var index = 0; index < _inputHandlers.Count; index++)
                Inputs[index].CurrentChanged -= _inputHandlers[index];
            
            _inputHandlers.Clear();
        }
    }
}