using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Services;

public class ScoreController : IService
{
    private CoinSpawner _coinSpawner;
    private readonly DataController _dataController;
    private int _currentScore = 0;

    public Action<int> OnScoreChange;

    public ScoreController(CoinSpawner coinSpawner, DataController dataController)
    {
        _coinSpawner = coinSpawner;
        _dataController = dataController;
        _coinSpawner.OnSplit = AddScore;
    }
    
    public void AddScore(int number)
    {
        _dataController.AddScore(number);
        OnScoreChange(_dataController._score);
    }
}