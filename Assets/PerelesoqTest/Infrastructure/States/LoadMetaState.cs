using System.Threading.Tasks;
using PerelesoqTest.Infrastructure.SceneManagement;
using PerelesoqTest.Infrastructure.States.Interfaces;

namespace PerelesoqTest.Infrastructure.States
{
    public class LoadMetaState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadMetaState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            // TODO: show curtain
            var sceneInstance = await _sceneLoader.Load("Meta", OnLoaded);
        }

        public void Exit()
        {

        }

        private async void OnLoaded(string sceneName)
        {
            await InitUIRoot();
            await InitMainMenu();
            
            _stateMachine.Enter<LoadLevelState, string>("gameplay level");
        }

        private async Task InitUIRoot()
        {

        }

        private async Task InitMainMenu()
        {

        }
    }
}