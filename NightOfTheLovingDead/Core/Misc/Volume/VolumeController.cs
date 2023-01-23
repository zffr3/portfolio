using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audio;

    [SerializeField]
    private bool _isMusic;

    // Start is called before the first frame update
    void Start()
    {
        OnVolumeChanged(this,this);

        EventBus.SubscribeToEvent(EventType.VOLUME_CHANGED, OnVolumeChanged);
        EventBus.SubscribeToEvent(EventType.MUSIC_CHANGED, OnVolumeChanged);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.VOLUME_CHANGED, OnVolumeChanged);
        EventBus.SubscribeToEvent(EventType.MUSIC_CHANGED, OnVolumeChanged);
    }

    private void OnVolumeChanged(object sender, object data)
    {
        if (this._isMusic)
        {
            this._audio.volume = CalculateSoundParam(this._audio.volume, VolumeData.MusicVolume);
        }
        else
        {
            this._audio.volume = CalculateSoundParam(this._audio.volume, VolumeData.Volume);
        }
    }

    private float CalculateSoundParam(float currentVolume, float newFloat)
    {
        if (currentVolume < newFloat)
        {
            return currentVolume + (newFloat - currentVolume);
        }
        else
        {
            return currentVolume - (currentVolume - newFloat);
        }
    }
}
