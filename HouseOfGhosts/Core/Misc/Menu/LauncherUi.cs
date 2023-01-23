using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LauncherUi : MonoBehaviour
{
    public static LauncherUi Instance { get; private set;}

    [SerializeField]
    private Slider _sceneLoadingProgress;

    private void Start()
    {
        Instance = this;
    }

    public void DisplayLoadingBar()
    {
        this._sceneLoadingProgress.gameObject.SetActive(true);
    }

    public void SetSceneLoadingProgress(float progressValue)
    {
        this._sceneLoadingProgress.value = progressValue;
    }

    public void SetNewSlider(Slider newSlider)
    {
        this._sceneLoadingProgress = newSlider;
    }
}
