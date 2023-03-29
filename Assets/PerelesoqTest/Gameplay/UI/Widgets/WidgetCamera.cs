using PerelesoqTest.Gameplay.Gadgets;
using UnityEngine;
using UnityEngine.UI;
using Camera = PerelesoqTest.Gameplay.Gadgets.Functions.Camera;

namespace PerelesoqTest.Gameplay.UI.Widgets
{
    public class WidgetCamera : WidgetBase
    {
        [SerializeField] private Button selectButton;

        public override void Initialize(GadgetBaseInfo gadgetInfo)
        {
            var camera = gadgetInfo.GetComponent<Camera>();
            
            selectButton.onClick.AddListener(() => camera.Interact());
            
            base.Initialize(gadgetInfo);
        }
    }
}