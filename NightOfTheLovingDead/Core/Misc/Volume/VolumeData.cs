using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VolumeData
{
    private static float _volume;
    private static float _musicVolume;

    public static float Volume
    {
        get 
        {
            return _volume;
        }
        set
        {
            _volume = value;
            EventBus.Dispath(EventType.VOLUME_CHANGED, VolumeData.Volume, value);
        }
    }

    public static float MusicVolume
    {
        get 
        {
            return _musicVolume;
        }
        set 
        {
            _musicVolume = value;
            EventBus.Dispath(EventType.MUSIC_CHANGED, VolumeData.MusicVolume, value);
        }
    }
}
