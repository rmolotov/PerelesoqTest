using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerelesoqTest.Gameplay.Gadgets;
using UnityEngine.ResourceManagement.ResourceProviders;
using PerelesoqTest.Infrastructure.Factories.Interfaces;
using PerelesoqTest.Infrastructure.SceneManagement;
using PerelesoqTest.Infrastructure.States.Interfaces;
using PerelesoqTest.Meta;
using PerelesoqTest.StaticData.Gadgets;
using PerelesoqTest.StaticData.Widgets;
using UnityEngine;

namespace PerelesoqTest.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string SceneName = "Level_test";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ILevelFactory _levelFactory;
        private readonly IUIFactory _uiFactory;

        private HudController _hud;

        public LoadLevelState(
            GameStateMachine gameStateMachine, 
            SceneLoader sceneLoader,
            ILevelFactory levelFactory,
            IUIFactory uiFactory)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _levelFactory = levelFactory;
            _uiFactory = uiFactory;
        }

        public async void Enter(string levelStaticData)
        {
            // TODO: show curtain
            
            await LoadScenesForLevel();
        }

        public void Exit()
        {

        }

        private async Task LoadScenesForLevel() =>
            await _sceneLoader
                .LoadSet(SceneName)
                .ContinueWith(task => OnLoaded(task.Result), TaskScheduler.FromCurrentSynchronizationContext());

        private async void OnLoaded(IReadOnlyDictionary<SceneLayerType, SceneInstance> loadedLayers)
        {
            var uiLayerScene = loadedLayers[SceneLayerType.UI];
            
            await InitUIRoot(uiLayerScene);
            _hud = await InitUI();
            
            await InitGameWold();
            
            _stateMachine.Enter<GameLoopState>();
        }
        
        private async Task InitUIRoot(SceneInstance uiLayerScene)
        {
            var uiRoot = await _uiFactory.CreateUIRoot();
            _sceneLoader.MoveGameObjectToScene(uiRoot.gameObject, uiLayerScene);
        }

        private async Task InitGameWold()
        {
            await SetupGadgets();
        }

        private async Task<HudController> InitUI() => 
            await _uiFactory.CreateHud();
        

        private async Task SetupGadgets()
        {
            var gadgets = await _levelFactory.CreateLevel();
            
            var displayedGadgets = gadgets
                .Where(g => g.WidgetType is not WidgetType.NONE);
            
            var electricityMeter = gadgets
                .Find(g => g.GadgetType == GadgetType.PowerSource)
                .GetComponent<ElectricityMeter>();

            SetupElectricityMeterUI(electricityMeter);
            await SetupWidgets(displayedGadgets);
        }

        private void SetupElectricityMeterUI(ElectricityMeter meter) => 
            _hud.Initialize(meter);

        private async Task SetupWidgets(IEnumerable<GadgetBaseInfo> displayedGadgets)
        {
            foreach (var gadget in displayedGadgets) 
                await _uiFactory.CreateWidget(forGadget: gadget);
        }
    }
}