using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    public static Launcher Instance { get; private set; }

    private AsyncOperation _sceneLoadOperation;

    private void Start()
    {
        Instance = this;
    }

    public void PlaySinglePlayerNewGame()
    {
        this._sceneLoadOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    public void ReturnToMainMenu()
    {
        this._sceneLoadOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    public void StartNextLevel()
    {
        this._sceneLoadOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        EventBus.Dispath(EventType.LEVEL_CLOSED, this,this);
    }
}
