using CodeBase.Infrastructure.Services;

namespace CoinMerge.States
{
    public class GameloopState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly AllServices _allServices;

        public GameloopState(StateMachine stateMachine, AllServices allServices)
        {
            _stateMachine = stateMachine;
            _allServices = allServices;
        }

        private void RegisterServices()
        {
            _allServices.RegisterSingle<CoinSpawner>(new CoinSpawner());
            _allServices.RegisterSingle<ScoreController>(new ScoreController(_allServices.Single<CoinSpawner>(), _allServices.Single<DataController>()));
            _allServices.RegisterSingle<EndGameController>(new EndGameController(_stateMachine));
            _allServices.RegisterSingle<UIController>(new UIController(_allServices.Single<ScoreController>()));
        }

        public void Enter()
        {
            RegisterServices();
        }

        public void Exit()
        {
            
        }
    }
}