using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CoinMerge.States;
using Infrastructure.AssetManagment;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

public class UIController : IService
{
    private readonly ScoreController _scoreController;
    private Text _countField;
    private UIView _uiView;

    public UIController(ScoreController scoreController)
    {
        _scoreController = scoreController;
        _uiView = AssetProvider.Instantiate(AssetPath.HUDPath).GetComponent<UIView>();
        _countField = _uiView.ScoreText;
        scoreController.OnScoreChange += UpdateScoreField;
        _uiView.ResumeButton.onClick.AddListener(ResumeGame);
        _uiView.StopButton.onClick.AddListener(StopGame);
        _uiView.ShowRecordsPanel.onClick.AddListener(ShowRecordsPanel);
        _uiView.HideRecordsPanel.onClick.AddListener(HideRecordsPanel);
        _uiView.ExitButton.onClick.AddListener(AllServices.Container.Single<EndGameController>().StartEndGame);
        AllServices.Container.Single<DataController>().GetUsersScoreList(OnScoreLoaded);

    }

    private void UpdateScoreField(int number)
    {
        _countField.text = number.ToString();
    }

    private void StopGame()
    {
        AllServices.Container.Single<CoinSpawner>().Platform.SetActive(false);
        Time.timeScale = 0;
        _uiView.PausePanel.SetActive(true);
    }

    private void ResumeGame()
    {
        AllServices.Container.Single<CoinSpawner>().Platform.SetActive(true);

        Time.timeScale = 1;
        _uiView.PausePanel.SetActive(false);
    }

    public void ShowRecordsPanel()
    {
        AllServices.Container.Single<CoinSpawner>().Platform.SetActive(false);
        _uiView.RecordsPanel.SetActive(true);
    }

    private void OnScoreLoaded(List<UserData> userData)
    {
        AllServices.Container.Single<DataController>().FillScorePanel(_uiView.UsersPanel, userData, "UserPanelGame");
    }

    public void HideRecordsPanel()
    {
        AllServices.Container.Single<CoinSpawner>().Platform.SetActive(true);
        _uiView.RecordsPanel.SetActive(false);
        
    }
}