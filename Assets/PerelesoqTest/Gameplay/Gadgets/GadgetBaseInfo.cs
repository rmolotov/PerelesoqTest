using UnityEngine;
using Sirenix.OdinInspector;
using PerelesoqTest.Gameplay.UI.Widgets;
using PerelesoqTest.StaticData.Gadgets;
using PerelesoqTest.StaticData.Widgets;

namespace PerelesoqTest.Gameplay.Gadgets
{
    [DisallowMultipleComponent]
    public class GadgetBaseInfo : MonoBehaviour
    {
        public string Id;
        public string DisplayName;
        public string Status;

        [EnumPaging] public GadgetType GadgetType;
        [EnumPaging] public WidgetType WidgetType;
    }
}