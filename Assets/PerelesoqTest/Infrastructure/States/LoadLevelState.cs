using System.Threading.Tasks;
using PerelesoqTest.Infrastructure.States.Interfaces;
using UnityEngine.SceneManagement;

namespace PerelesoqTest.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private const string SceneName = "Level_test";

        public LoadLevelState(GameStateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }

        public async void Enter(string levelStaticData)
        {
            // TODO: show curtain
            
            LoadScenesForLevel();

            OnLoaded();
        }

        public void Exit()
        {

        }

        private void LoadScenesForLevel()
        {
            // loader: load test scene, callback = onloaded, tmp:
            
            SceneManager.LoadScene(SceneName);
            SceneManager.LoadScene($"{SceneName}_Env", LoadSceneMode.Additive);
            SceneManager.LoadScene($"{SceneName}_Lights", LoadSceneMode.Additive);
            SceneManager.LoadScene($"{SceneName}_Nav", LoadSceneMode.Additive);
            SceneManager.LoadScene($"{SceneName}_UI", LoadSceneMode.Additive);
        }

        private async void OnLoaded()
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