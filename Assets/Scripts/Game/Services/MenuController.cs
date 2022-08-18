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
        var uiView = AssetProvider.Instantiate(AssetPath.MenuHUD).GetComponent<MenuUIView>();
        uiView.PlayButton.onClick.AddListener(LoadNextScene);
        uiView.ShareButton.onClick.AddListener(ShareGameForFriends);
        _scorePanel = uiView.UsersPanel;
    }

    public void FillScorePanel(List<UserData> userDatas)
    {
        foreach (var user in userDatas)
        {
            var userPanel = AssetProvider.Instantiate("UserPanel", Vector3.zero, _scorePanel);
            var userView = userPanel.GetComponent<UserPanelView>();
            userView.Name.text = user.UserName;
            userView.Score.text = user.Score.ToString();
            Application.ExternalEval("alert('SpawnNewUser')");
            if(user.UserPhotoLink != "") 
                AllServices.Container.Single<DataController>().SetImageFromServer(user.UserPhotoLink, userView.Photo);
        }
        
        
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