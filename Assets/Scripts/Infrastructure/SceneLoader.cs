using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }
    
    public async void Load(string name, Action onLoad)
    {
        _coroutineRunner.StartCoroutine(LoadScene(name, onLoad));
    }
    
    private IEnumerator LoadScene(string name, Action onLoaded)
    {
        if (SceneManager.GetActiveScene().name == name)
        {
            onLoaded?.Invoke();
            yield break;
        } 
        
        var awaitScene = SceneManager.LoadSceneAsync(name);
        while (!awaitScene.isDone)
        {
            yield return null;
        }
        
        onLoaded?.Invoke();
    }
}