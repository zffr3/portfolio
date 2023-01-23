using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance { get; private set; }

    [SerializeField]
    private AudioSource _musicSrc;

    [SerializeField]
    private AudioClip _ambientClip;

    [SerializeField]
    private AudioClip _huntingClip;

    [SerializeField]
    private AudioClip _demonDeathScream;

    [SerializeField]
    private AudioClip _demonKillScream;

    [SerializeField]
    private float _huntingVolumeValue;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        EventBus.SubscribeToEvent(EventType.START_HUNTING, OnHuntingStart);
        EventBus.SubscribeToEvent(EventType.STOP_HUNTING, OnHuntingEnd);

        OnHuntingEnd(null,null);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.START_HUNTING, OnHuntingStart);
        EventBus.UnsubscribeFromEvent(EventType.STOP_HUNTING, OnHuntingEnd);
    }

    private void OnHuntingStart(object sender, object param)
    {
        this._musicSrc.volume += this._huntingVolumeValue;

        this._musicSrc.Stop();
        this._musicSrc.clip = this._huntingClip;
        this._musicSrc.Play();
    }

    private void OnHuntingEnd(object sender, object param)
    {
        if (this._musicSrc.volume - this._huntingVolumeValue != 0)
        {
            this._musicSrc.volume -= this._huntingVolumeValue;
        }

        this._musicSrc.Stop();
        this._musicSrc.clip = this._ambientClip;
        this._musicSrc.Play();
    }

    public void PlayDeathScream()
    {
        this._musicSrc.Stop();
        this._musicSrc.volume *= 6;
        this._musicSrc.clip = this._demonDeathScream;
        this._musicSrc.loop = false;
        this._musicSrc.Play();
    }

    public void PlayKillScream()
    {
        this._musicSrc.Stop();
        this._musicSrc.clip = this._demonKillScream;
        this._musicSrc.loop = false;
        this._musicSrc.Play();
    }
}
