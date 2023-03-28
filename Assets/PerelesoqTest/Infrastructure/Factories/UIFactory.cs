using System.Threading.Tasks;
using PerelesoqTest.Gameplay.UI.Widgets;
using PerelesoqTest.Infrastructure.AssetManagement;
using PerelesoqTest.Infrastructure.Factories.Interfaces;
using PerelesoqTest.Meta;
using PerelesoqTest.StaticData.Gadgets;
using UnityEngine;
using Zenject;

namespace PerelesoqTest.Infrastructure.Factories
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPrefabId     = "UIRootPrefab";
        private const string HudPrefabId        = "HudPrefab";
        private const string WidgetPrefabPrefix = "Device_";

        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;
        
        private Canvas _uiRoot;

        public UIFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }

        public async Task WarmUp()
        {
            await _assetProvider.Load<GameObject>(key: UIRootPrefabId);
            await _assetProvider.Load<GameObject>(key: HudPrefabId);
        }

        public void CleanUp()
        {
            
        }

        public async Task<Canvas> CreateUIRoot()
        {
            var prefab = await _assetProvider.Load<GameObject>(key: UIRootPrefabId);
            return _uiRoot = Object.Instantiate(prefab).GetComponent<Canvas>();
        }

        public async Task<HudController> CreateHud()
        {
            var prefab = await _assetProvider.Load<GameObject>(key: HudPrefabId);
            var hud = Object.Instantiate(prefab, _uiRoot.transform).GetComponent<HudController>();

            _container.Inject(hud);
            return hud;
        }

        public async Task<WidgetBase> CreateWidget(GadgetType forGadget)
        {
            var prefabKey = WidgetPrefabPrefix + forGadget;
            var prefab = await _assetProvider.Load<GameObject>(key: prefabKey);
            
            var widget = Object.Instantiate(prefab).GetComponent<WidgetBase>();
            widget.Initialize();

            return widget;
        }
    }
}