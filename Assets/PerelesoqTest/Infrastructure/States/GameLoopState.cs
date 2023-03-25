using PerelesoqTest.Infrastructure.States.Interfaces;

namespace PerelesoqTest.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _stateMachine = gameStateMachine;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            // clean up gameplay factories
        }
    }
}