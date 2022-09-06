using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CoinMerge.States;
using Infrastructure.AssetManagment;
using UnityEngine;
using UnityEngine.UI;

public class Runpoint : MonoBehaviour, ICoroutineRunner
{
    private Game _game;
    
    private void Awake()
    {
        Time.timeScale = 1;
        AllServices.Container.RegisterSingle<IAsset>(new AssetProvider());

        AllServices.Container.RegisterSingle<DataController>(new DataController(this));

        Debug.Log("AAA");
        StartDataSet();

    }

    public void SetUserId(string userSet)
    {
        Debug.Log(userSet);

        var data = AllServices.Container.Single<DataController>();
        data.SetUserInfo(userSet);
        StartDataSet();
    }
    
    public void SetMessageId(string messageId)
    {
        Debug.Log(messageId);

        var data = AllServices.Container.Single<DataController>();
        data.SetMessageInfo(messageId);
    }

    public void SetChatId(string chatId)
    {
        Debug.Log(chatId);

        var data = AllServices.Container.Single<DataController>();
        data.SetChatInfo(chatId);
    }

    public void StartDataSet()
    {
        _game = new Game(this);
        _game.StateMachine.Enter<BootstrapState>();
        DontDestroyOnLoad(this);
    }
}

