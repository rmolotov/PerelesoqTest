using PerelesoqTest.StaticData.Gadgets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PerelesoqTest.Gameplay.Gadgets
{
    [DisallowMultipleComponent]
    public class GadgetBaseInfo : MonoBehaviour
    {
        public string Id;
        public string DisplayName;
        public string Status;

        [EnumPaging] 
        public GadgetType GadgetType;
    }
}