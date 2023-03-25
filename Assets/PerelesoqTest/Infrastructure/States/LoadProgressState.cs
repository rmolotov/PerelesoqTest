using PerelesoqTest.Infrastructure.States.Interfaces;

namespace PerelesoqTest.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public LoadProgressState(GameStateMachine gameStateMachine){
        {
            _stateMachine = gameStateMachine;
        }}

        public void Enter()
        {
            LoadProgressOrInitNew();

            _stateMachine.Enter<LoadMetaState>();
        }

        public void Exit()
        {
            
        }

        private async void LoadProgressOrInitNew()
        {

        }
    }
}