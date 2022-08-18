using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using Infrastructure.AssetManagment;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

public class DataController: IService
{
    private readonly Runpoint _runpoint;
    private readonly StateMachine _stateMachine;
    private NetworkController _networkController;
    
    private string _messageId = "AgAAAD1wAAAao5ZUgqt-Y5mDhVM";//test values
    private string _userId = "1419158298"; //test values
    private string _chatId = "-1929436822581208416";//test values
    public int _score = 0;
    public List<UserData> ScoresData;

    [DllImport("__Internal")]
    private static extern void Share();

    public DataController(Runpoint runpoint)
    {
        _runpoint = runpoint;
        _networkController = new NetworkController();
    }

    public void SetUserInfo(string userId)
    {
        _userId = userId;
    }

    public void SetMessageInfo(string messageId)
    {
        _messageId = messageId;
    }

    public void AddScore(int count)
    {
        _score += count;
    }

    public void SetChatInfo(string chatId)
    {
        _chatId = chatId;
    }
    
    public async void SendScoreToServer()
    {
        var dataToJson = new UserData();
        dataToJson.ChatId = long.Parse(_chatId);
        dataToJson.Score = _score;
        dataToJson.MessageId = _messageId;
        dataToJson.UserId = long.Parse(_userId);
        _runpoint.StartCoroutine(_networkController.SendUserScoreToServer(dataToJson));
    }
    
    public void FillScorePanel(Transform _scorePanel, List<UserData> data, string userPanelName)
    {
        var count = 0;
        foreach (var user in data)
        {
            count++;
            
            var userPanel = AssetProvider.Instantiate(userPanelName, Vector3.zero, _scorePanel);
            var userView = userPanel.GetComponent<UserPanelView>();
            userView.Iterator.text = count.ToString();
            userView.Name.text = user.UserName;
            if (user.Score > 1000)
            {
                userView.Score.text = (user.Score / 1000).ToString()+"k";
            }
            if(user.UserPhotoLink != "") 
                AllServices.Container.Single<DataController>().SetImageFromServer(user.UserPhotoLink, userView.Photo);
        }
    }

    public async void GetUsersScoreList(Action<List<UserData>> onLoad)
    {
        Debug.Log("AAA");
        var dataToJson = new UserData();
        dataToJson.UserId = long.Parse(_userId);
        dataToJson.ChatId = long.Parse(_chatId);
        dataToJson.MessageId = _messageId;
        _runpoint.StartCoroutine(_networkController.GetScoreListFromServer(dataToJson, onLoad));
    }

    public async void SetImageFromServer(string url, RawImage image)
    {
        _runpoint.StartCoroutine(_networkController.DownloadImage(url, image));
    }
    
    public void ShareGame()
    {
        Share();
    }
}