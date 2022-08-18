using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CoinMerge.States;
using Infrastructure.AssetManagment;
using Infrastructure.Services;
using UnityEngine;

public class MenuController : IService
{
    private readonly StateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private Transform _scorePanel;
    public MenuController(StateMachine stateMachine, SceneLoader sceneLoader)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        
    }

    public void LoadMenu()
    {
        var uiView = AssetProvider.Instantiate(AssetPath.MenuHUD).GetComponent<MenuUIView>();
        uiView.PlayButton.onClick.AddListener(LoadNextScene);
        uiView.ShareButton.onClick.AddListener(ShareGameForFriends);
        _scorePanel = uiView.UsersPanel;
        var data = AllServices.Container.Single<DataController>();
        data.GetUsersScoreList(OnScoreLoaded);
        
    }

    private void OnScoreLoaded(List<UserData> data)
    {
        var dataController = AllServices.Container.Single<DataController>();
        dataController.FillScorePanel(_scorePanel, data, "UserPanel");
    }

    private void ShareGameForFriends()
    {
        AllServices.Container.Single<DataController>().ShareGame();
    }

    private void LoadNextScene()
    {
        _sceneLoader.Load("GameScene", EnterNextState);
    }

    private void EnterNextState()
    {
        _stateMachine.Enter<GameloopState>();
    }
    
    
}