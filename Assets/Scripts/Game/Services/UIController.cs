using CodeBase.Infrastructure.Services;
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
}