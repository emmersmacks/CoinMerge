using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using Infrastructure.AssetManagment;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CoinMerge.States
{
    public class BootstrapState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly AllServices _allServices;
        private readonly Runpoint _runpoint;

        public BootstrapState(StateMachine stateMachine, AllServices allServices, Runpoint runpoint)
        {
            _stateMachine = stateMachine;
            _allServices = allServices;
            _runpoint = runpoint;
            RegisterServices();
        }

        private void RegisterServices()
        {
        }

        public void Enter()
        {
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                _stateMachine.Enter<MenuState>();
            }
            else if (SceneManager.GetActiveScene().name == "GameScene")
            {
                _stateMachine.Enter<GameloopState>();
            }
            else
            {
                _stateMachine.Enter<MenuState>();
            }
        }

        public void Exit()
        {
            
        }
    }
}