using System;
using UnityEngine;

namespace PerelesoqTest.Gameplay.Gadgets.Ports
{
    [DisallowMultipleComponent]
    public class OutputPort : MonoBehaviour
    {
        public int Current;
        public Action CurrentChanged;
    }
}