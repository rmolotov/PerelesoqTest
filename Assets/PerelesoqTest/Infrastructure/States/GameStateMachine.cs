using System;
using System.Collections.Generic;
using PerelesoqTest.Infrastructure.Factories.Interfaces;
using PerelesoqTest.Infrastructure.SceneManagement;
using PerelesoqTest.Infrastructure.States.Interfaces;
using PerelesoqTest.Services.Logging;
using Zenject;

namespace PerelesoqTest.Infrastructure.States
{
    public class GameStateMachine : IInitializable
    {
        private readonly ILoggingService _logger;
        
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(SceneLoader sceneLoader, ILoggingService logger, IUIFactory uiFactory)
        {
            _logger = logger;
            
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)]    = new BootstrapState(this),
                [typeof(LoadProgressState)] = new LoadProgressState(this),
                [typeof(LoadMetaState)]     = new LoadMetaState(this, sceneLoader),
                [typeof(LoadLevelState)]    = new LoadLevelState(this, sceneLoader, uiFactory),
                [typeof(GameLoopState)]     = new GameLoopState(this),
            };
        }

        public void Initialize() => 
            Enter<BootstrapState>();

        public void Enter<TState>() where TState : class, IState
        {
            ChangeState<TState>()
                .Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            ChangeState<TState>()
                .Enter(payload);
        }


        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            var state = GetState<TState>();
            _currentState = state;

            _logger.LogMessage($"state changed to {_currentState.GetType().Name}", nameof(GameStateMachine));
            
            return state;
        }
    }
}