using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PerelesoqTest.Gameplay.Gadgets.Functions
{
    public class Camera : BaseFunction
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        #region DrawStaatusInEditor

        [TitleGroup("Actions/Status")]
        [ShowInInspector]
        [
            Button(
                "@CinemachineBrain.SoloCamera == virtualCamera ? \"LIVE\" : \"DISABLED\"",
                Icon = SdfIconType.Record2, IconAlignment = IconAlignment.LeftOfText, 
                Stretch = false, ButtonAlignment = 0), 
            GUIColor("@CinemachineBrain.SoloCamera == virtualCamera ? Color.red : Color.gray")
        ]
        private void IsLive() { }

        #endregion


        public override void Interact()
        {
            CinemachineBrain.SoloCamera = virtualCamera;
            base.Interact();
        }

        protected override void ReportStatus(bool state)
        {
            
        }
    }
}