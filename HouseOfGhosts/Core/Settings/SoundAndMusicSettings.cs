using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundAndMusicSettings : MonoBehaviour
{
    public static SoundAndMusicSettings Instance;

    [SerializeField]
    private Slider _musicVolumeSlider;
    [SerializeField]
    private Slider _soundVolumeSlider;

    private float _musicVolume;
    private float _soundVolume;

    public float MusicVolume
    {
        get
        {
            return this._musicVolume;
        }
        private set
        {
            if (this._musicVolume != value)
            {
                PlayerPrefs.SetFloat("Music", value);
                this._musicVolume = value;
                EventBus.Dispath(EventType.MUSICVOLUME_CHANGED, this, this._musicVolume);
            }
        }
    }

    public float SoundVolume
    {
        get 
        {
            return this._soundVolume;
        }
        private set
        {
            if (value != this._soundVolume)
            {
                PlayerPrefs.SetFloat("Sound", value);
                this._soundVolume = value;
                EventBus.Dispath(EventType.SOUNDVOLUME_CHANGED, this, this._soundVolume);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        this._musicVolume = PlayerPrefs.GetFloat("Music");
        this._soundVolume = PlayerPrefs.GetFloat("Sound");

        if (this._musicVolume == 0)
        {
            SetMusicVolume(0.1f);
        }
        if (this._soundVolume == 0)
        {
            SetSoundVolume(0.1f);
        }

        if (this._musicVolumeSlider != null)
        {
            this._musicVolumeSlider.value = this._musicVolume;
        }
        if (this._soundVolumeSlider != null)
        {
            this._soundVolumeSlider.value = this._soundVolume;
        }

        EventBus.Dispath(EventType.MUSICVOLUME_CHANGED, this, this.MusicVolume);
        EventBus.Dispath(EventType.SOUNDVOLUME_CHANGED, this, this.SoundVolume);
    }

    public void SetMusicVolume()
    {
        SetMusicVolume(this._musicVolumeSlider.value);
    }

    public void SetSoundVolume()
    {
        SetSoundVolume(this._soundVolumeSlider.value);
    }

    private void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
    }

    private void SetSoundVolume(float volume)
    {
        SoundVolume = volume;
    }
}
