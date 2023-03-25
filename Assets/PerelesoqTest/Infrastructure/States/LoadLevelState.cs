using System.Threading.Tasks;
using PerelesoqTest.Infrastructure.SceneManagement;
using PerelesoqTest.Infrastructure.States.Interfaces;

namespace PerelesoqTest.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string SceneName = "Level_test";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public async void Enter(string levelStaticData)
        {
            // TODO: show curtain
            
            await LoadScenesForLevel();
        }

        public void Exit()
        {

        }

        private async Task LoadScenesForLevel()
        {
            var sceneInstances = await _sceneLoader.LoadSet(SceneName, OnLoaded);
            // todo: setup layers
        }

        private async void OnLoaded(string sceneName)
        {
            await InitUIRoot();
            await InitGameWold();
            await InitUI();
            _stateMachine.Enter<GameLoopState>();
        }
        
        private async Task InitUIRoot()
        {

        }

        private async Task InitGameWold()
        {
            SetupCamera();
            await SetupGadgets();
        }

        private async Task InitUI()
        {

        }

        private void SetupCamera()
        {

        }

        private async Task SetupGadgets()
        {
            
        }
    }
}