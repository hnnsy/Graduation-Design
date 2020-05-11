using System;
using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Init : MonoBehaviour
{
    public GameObject loadingScreen;
    [SceneName]
    public string loadingLevel;
    public Slider slider;
    private void Awake()
    {
        
    }

    private void startmenu()
    {
        StartCoroutine(LoadingLevelI());
    }
    
    IEnumerator LoadingLevelI()
    {
        GameSaveManager.Instance.LoadGameData();
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadingLevel);
        
        
        while (!operation.isDone)
        {
            slider.value = operation.progress;
            AudioManager.Instance.ChangeBackgroundSound("Title");
            yield return null;
        }
    }
}
