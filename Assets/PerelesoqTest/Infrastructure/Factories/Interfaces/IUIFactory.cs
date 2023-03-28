using System.Threading.Tasks;
using PerelesoqTest.Gameplay.UI.Widgets;
using PerelesoqTest.Meta;
using PerelesoqTest.StaticData.Gadgets;
using UnityEngine;

namespace PerelesoqTest.Infrastructure.Factories.Interfaces
{
    public interface IUIFactory
    {
        Task WarmUp();
        void CleanUp();
        
        Task<Canvas> CreateUIRoot();
        Task<HudController> CreateHud();
        Task<WidgetBase> CreateWidget(GadgetType forGadget);
    }
}