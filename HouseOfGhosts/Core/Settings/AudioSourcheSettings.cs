using UnityEngine;

public class AudioSourcheSettings : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioSettingsType _currentType;

    // Start is called before the first frame update
    void Start()
    {
        if (this._currentType == AudioSettingsType.Sound)
        {
            if(SoundAndMusicSettings.Instance != null)
            {
                this._audioSource.volume = SoundAndMusicSettings.Instance.SoundVolume;
            }

            EventBus.SubscribeToEvent(EventType.SOUNDVOLUME_CHANGED, ChangeAudioSourceVolume);
        }
        else
        {
            if (SoundAndMusicSettings.Instance != null)
            {
                this._audioSource.volume = SoundAndMusicSettings.Instance.MusicVolume;
            }

            EventBus.SubscribeToEvent(EventType.MUSICVOLUME_CHANGED, ChangeAudioSourceVolume);
        }
    }

    private void OnDestroy()
    {
        if (this._currentType == AudioSettingsType.Sound)
        {
            EventBus.UnsubscribeFromEvent(EventType.SOUNDVOLUME_CHANGED, ChangeAudioSourceVolume);
        }
        else
        {
            EventBus.UnsubscribeFromEvent(EventType.MUSICVOLUME_CHANGED, ChangeAudioSourceVolume);
        }
    }

    private void ChangeAudioSourceVolume(object sender, object param)
    {
        this._audioSource.volume = (float)param;
    }

    private enum AudioSettingsType
    {
        Music,
        Sound
    }
}
