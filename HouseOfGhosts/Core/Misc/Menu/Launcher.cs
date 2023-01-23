using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    public static Launcher Instance { get; private set; }

    private bool _sceneLoading;
    private AsyncOperation _sceneLoadOperation;

    private void Start()
    {
        Instance = this;
    }

    public void PlaySinglePlayerNewGame()
    {
        this._sceneLoading = true;
        this._sceneLoadOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    public void PlayTraining()
    {
        this._sceneLoading = true;
        this._sceneLoadOperation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
    }

    public void ReturnToMainMenu()
    {
        this._sceneLoading = true;
        this._sceneLoadOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    public void PlayFinalScene()
    {
        this._sceneLoading = true;
        this._sceneLoadOperation = SceneManager.LoadSceneAsync(3, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (this._sceneLoading)
        {
            LauncherUi.Instance.SetSceneLoadingProgress(Mathf.Clamp01(this._sceneLoadOperation.progress / 0.9f));
        }
    }
}
