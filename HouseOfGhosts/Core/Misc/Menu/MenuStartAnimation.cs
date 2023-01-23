using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuStartAnimation : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSrc;

    [SerializeField]
    private AudioClip _btnClip;
    [SerializeField]
    private AudioClip _injectClip;

    [SerializeField]
    private GameObject _redLightObj;
    [SerializeField]
    private GameObject _greenLightObj;

    [SerializeField]
    private TMP_Text _titleText;

    [SerializeField]
    private GameObject _menuObj;

    [SerializeField]
    private GameObject _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.SubscribeToEvent(EventType.GAMELOGO_CLOSED, OnGamelogoEnded);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.GAMELOGO_CLOSED, OnGamelogoEnded);
    }

    private void OnGamelogoEnded(object sender, object param)
    {
        StartCoroutine(ActivateButton());
    }

    IEnumerator ActivateButton()
    {
        this._mainCamera.SetActive(true);
        this._audioSrc.clip = this._btnClip;
        this._audioSrc.Play();
        yield return new WaitForSecondsRealtime(this._btnClip.length);

        this._redLightObj.SetActive(false);
        this._greenLightObj.SetActive(true);

        yield return new WaitForSecondsRealtime(this._btnClip.length);

        StartCoroutine(InjectVHS());
    }

    IEnumerator InjectVHS()
    {
        this._audioSrc.clip = this._injectClip;
        this._audioSrc.Play();
        yield return new WaitForSecondsRealtime(this._injectClip.length);
        this._titleText.text = "House of ghosts";
        this._menuObj.SetActive(true);
    }
}
