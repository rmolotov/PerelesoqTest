using TMPro;
using UnityEngine;

namespace PerelesoqTest.Gameplay.UI.Widgets
{
    public abstract class WidgetBase : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI nameText;
        [SerializeField] protected TextMeshProUGUI statusText;
        
        public void Initialize()
        {
            
        }
    }
}