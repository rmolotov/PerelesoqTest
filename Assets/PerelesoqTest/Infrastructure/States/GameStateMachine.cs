﻿using System;
using System.Collections.Generic;
using PerelesoqTest.Infrastructure.States.Interfaces;
using Zenject;

namespace PerelesoqTest.Infrastructure.States
{
    public class GameStateMachine : IInitializable
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)]    = new BootstrapState(this),
                [typeof(LoadProgressState)] = new LoadProgressState(this),
                [typeof(LoadMetaState)]     = new LoadMetaState(this),
                [typeof(LoadLevelState)]    = new LoadLevelState(this),
                [typeof(GameLoopState)]     = new GameLoopState(this),
            };
        }

        public void Initialize() => 
            Enter<BootstrapState>();

        public void Enter<TState>() where TState : class, IState =>
            ChangeState<TState>()
                .Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> =>
            ChangeState<TState>()
                .Enter(payload);


        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            var state = GetState<TState>();
            _currentState = state;

            return state;
        }
    }
}