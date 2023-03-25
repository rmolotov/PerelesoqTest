using PerelesoqTest.Infrastructure.States.Interfaces;

namespace PerelesoqTest.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
            
        }
    }
}