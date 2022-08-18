using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CoinMerge.States
{
    public class MenuState: IState
    {
        private readonly StateMachine _stateMachine;
        private readonly AllServices _allServices;
        private readonly SceneLoader _sceneLoader;

        public MenuState(StateMachine stateMachine, AllServices allServices, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _allServices = allServices;
            _sceneLoader = sceneLoader;
            RegisterServices();

        }
        
        public void Enter()
        {
            if (SceneManager.GetActiveScene().name != "Menu")
            {
                _sceneLoader.Load("Menu", OnLoadMenu);
            }
            else
            {
                AllServices.Container.Single<MenuController>().LoadMenu();
            }
            
        }

        private void OnLoadMenu()
        {
            AllServices.Container.Single<MenuController>().LoadMenu();
        }

        public void Exit()
        {
            
        }
        
        private void RegisterServices()
        {
            _allServices.RegisterSingle(new MenuController(_stateMachine, _sceneLoader));
        }
    }
}