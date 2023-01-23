using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TrailerVolume : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer _trailerSrc;

    // Start is called before the first frame update
    void Start()
    {
        OnVolumeChanged(this,this);
        EventBus.SubscribeToEvent(EventType.MUSIC_CHANGED, OnVolumeChanged);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.MUSIC_CHANGED, OnVolumeChanged);
    }

    private void OnVolumeChanged(object sender, object param)
    {
        this._trailerSrc.SetDirectAudioVolume(0, CalculateSoundParam(this._trailerSrc.GetDirectAudioVolume(0),VolumeData.MusicVolume));
    }

    private float CalculateSoundParam(float currentVolume, float newVolume)
    {
        if (currentVolume < newVolume)
        {
            return currentVolume + (newVolume - currentVolume);
        }
        else
        {
            return currentVolume - (currentVolume - newVolume);
        }
    }
}
