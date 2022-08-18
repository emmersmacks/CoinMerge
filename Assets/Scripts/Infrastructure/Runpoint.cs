using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CoinMerge.States;
using UnityEngine;
using UnityEngine.UI;

public class Runpoint : MonoBehaviour, ICoroutineRunner
{
    private Game _game;

    private void Awake()
    {
        Time.timeScale = 1;

        _game = new Game(this);
        _game.StateMachine.Enter<BootstrapState>();
        DontDestroyOnLoad(this);
        var data = AllServices.Container.Single<DataController>();

        data.GetUsersScoreList();

    }

    public void SetUserId(string userSet)
    {
        var data = AllServices.Container.Single<DataController>();
        data.SetUserInfo(userSet);
    }
    
    public void SetMessageId(string messageId)
    {
        var data = AllServices.Container.Single<DataController>();
        data.SetMessageInfo(messageId);
    }

    public void SetChatId(string chatId)
    {
        var data = AllServices.Container.Single<DataController>();
        data.SetChatInfo(chatId);
    }
}

