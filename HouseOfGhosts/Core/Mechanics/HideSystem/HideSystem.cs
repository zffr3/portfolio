using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _lockerCamera;

    [SerializeField]
    private HideTrigger _trigger;
    
    [SerializeField]
    private AudioSource _audioSrc;

    [SerializeField]
    private Animator _animSrc;

    private bool _isHunting;

    private void Start()
    {
        this._audioSrc = GetComponent<AudioSource>();

        EventBus.SubscribeToEvent(EventType.HIDE_PLAER, OnPlayerHiden);
        EventBus.SubscribeToEvent(EventType.RELEASE_PLAYER, OnPlayerRealised);
        EventBus.SubscribeToEvent(EventType.START_HUNTING, HandleStartHunting);
        EventBus.SubscribeToEvent(EventType.STOP_HUNTING, HandleEndHunting);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.HIDE_PLAER, OnPlayerHiden);
        EventBus.UnsubscribeFromEvent(EventType.RELEASE_PLAYER, OnPlayerRealised);
        EventBus.UnsubscribeFromEvent(EventType.START_HUNTING, HandleStartHunting);
        EventBus.UnsubscribeFromEvent(EventType.STOP_HUNTING, HandleEndHunting);
    }

    public void ChangeCameraState(bool newCamState)
    {
        this._lockerCamera.SetActive(newCamState);
    }

    private void OnPlayerHiden(object sender, object param)
    {
        if (this._trigger.GetPlayerStatus())
        {
            this._audioSrc.Play();
            ChangeCameraState(true);
            this._trigger.ChangeHidenState(true);
        }
    }

    private void OnPlayerRealised(object sender, object param)
    {
        if (this._trigger.GetPlayerStatus()) 
        {
            this._audioSrc.Play();
            ChangeCameraState(false);
            this._trigger.ChangeHidenState(false);
        }
    }

    private void HandleStartHunting(object sender,object param)
    {
        this._isHunting = true; 
        this._animSrc.SetBool("Blinking", true);
    }

    private void HandleEndHunting(object sender, object param)
    {
        this._isHunting=false;
        this._animSrc.SetBool("Blinking", false);
    }

    public bool GetHuntingStatus()
    {
        return this._isHunting;
    }
}
