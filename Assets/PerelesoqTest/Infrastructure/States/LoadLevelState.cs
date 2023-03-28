using System.Collections.Generic;
using System.Threading.Tasks;
using PerelesoqTest.Infrastructure.Factories.Interfaces;
using PerelesoqTest.Infrastructure.SceneManagement;
using PerelesoqTest.Infrastructure.States.Interfaces;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace PerelesoqTest.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string SceneName = "Level_test";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(
            GameStateMachine gameStateMachine, 
            SceneLoader sceneLoader,
            IUIFactory uiFactory)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
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
            await InitGameWold();
            await InitUI();
            _stateMachine.Enter<GameLoopState>();
        }
        
        private async Task InitUIRoot(SceneInstance uiLayerScene)
        {
            var uiRoot = await _uiFactory.CreateUIRoot();
            SceneManager.MoveGameObjectToScene(uiRoot.gameObject, uiLayerScene.Scene);
        }

        private async Task InitGameWold()
        {
            SetupCamera();
            await SetupGadgets();
        }

        private async Task InitUI() => 
            await _uiFactory.CreateHud();

        private void SetupCamera()
        {

        }

        private async Task SetupGadgets()
        {
            
        }
    }
}