﻿using CodeBase.Infrastructure.Services;
using Infrastructure.AssetManagment;
using Infrastructure.Services;

namespace CoinMerge.States
{
    internal class EndGameController : IService
    {
        private readonly StateMachine _stateMachine;

        public EndGameController(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            var deadLine = AssetProvider.Instantiate("DeadLine");
            deadLine.GetComponent<EndScript>().OnTriggered += StartEndGame;
        }

        private void StartEndGame()
        {
            AllServices.Container.Single<DataController>().SendScoreToServer();
            _stateMachine.Enter<MenuState>();
        }
    }
}