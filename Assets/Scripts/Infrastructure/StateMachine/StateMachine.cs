using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CoinMerge.States;

public class StateMachine : IStateMachine
{
    private readonly AllServices _allServices;
    private readonly Runpoint _runpoint;
    private Dictionary<Type, IState> _states;
    private IState _currentState;

    public StateMachine(AllServices allServices, SceneLoader sceneLoader, Runpoint runpoint)
    {
        _allServices = allServices;
        _runpoint = runpoint;
        _states = new Dictionary<Type, IState>();
        _states.Add(typeof(BootstrapState), new BootstrapState(this, allServices,runpoint));
        _states.Add(typeof(MenuState), new MenuState(this, allServices, sceneLoader));
        _states.Add(typeof(GameloopState), new GameloopState(this, allServices));
    }
    
    public void Enter<SType>()
    {
        var state = _states[typeof(SType)];
        _currentState?.Exit();
        state.Enter();
        _currentState = state;
    }

    public void Exit<SType>()
    {
        var state = _states[typeof(SType)];
        state.Exit();
    }
}