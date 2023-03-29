using System;
using UnityEngine;
using Sirenix.OdinInspector;
using PerelesoqTest.StaticData.Gadgets;
using PerelesoqTest.StaticData.Widgets;

namespace PerelesoqTest.Gameplay.Gadgets
{
    [DisallowMultipleComponent]
    public class GadgetBaseInfo : MonoBehaviour
    {
        public string Id;
        public string DisplayName;
        
        [HideInInspector] 
        public object Status;
        
        public Action<object> StatusChanged;

        [EnumPaging] public GadgetType GadgetType;
        [EnumPaging] public WidgetType WidgetType;
    }
}