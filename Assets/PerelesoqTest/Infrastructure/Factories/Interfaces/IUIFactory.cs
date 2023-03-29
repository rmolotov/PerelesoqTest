using System.Threading.Tasks;
using UnityEngine;
using PerelesoqTest.Gameplay.Gadgets;
using PerelesoqTest.Gameplay.UI.Widgets;
using PerelesoqTest.Meta;

namespace PerelesoqTest.Infrastructure.Factories.Interfaces
{
    public interface IUIFactory
    {
        Task WarmUp();
        void CleanUp();
        
        Task<Canvas> CreateUIRoot();
        Task<HudController> CreateHud();
        Task<WidgetBase> CreateWidget(GadgetBaseInfo forGadget);
    }
}