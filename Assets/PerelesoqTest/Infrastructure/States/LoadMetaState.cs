using System.Threading.Tasks;
using PerelesoqTest.Infrastructure.States.Interfaces;

namespace PerelesoqTest.Infrastructure.States
{
    public class LoadMetaState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public LoadMetaState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async void Enter()
        {
            // TODO: show curtain

            //var sceneInstance = await _sceneLoader.Load(SceneName.Meta, OnLoaded);
            _stateMachine.Enter<LoadLevelState, string>("gameplay level");
        }

        public void Exit()
        {

        }

        private async void OnLoaded()
        {
            await InitUIRoot();
            await InitMainMenu();
        }

        private async Task InitUIRoot()
        {

        }

        private async Task InitMainMenu()
        {

        }
    }
}