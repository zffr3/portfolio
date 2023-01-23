using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField]
    private RadioTrigger _linkedTrigger;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private bool _acitvated;

    [SerializeField]
    private AudioClip _demonClip;
    [SerializeField]
    private AudioClip _noiseClip;

    private void Start()
    {
        this._acitvated = false;
        this._audioSource = GetComponent<AudioSource>();
    }


    public bool GetRadioStatus()
    {
        return this._acitvated;
    }

    public void ActiveRadio()
    {
        if (this._acitvated)
        {
            return;
        }

        this._acitvated = true;
        this._audioSource.clip = this._noiseClip;
        this._audioSource.Play();

        EventBus.Dispath(EventType.RADIO_ACTIVATED, this, this);

        this._linkedTrigger.ActivateRadio();
    }

    public void PlayDemonClip()
    {
        this._audioSource.Stop();
        this._audioSource.volume *= 2;

        this._audioSource.clip = this._demonClip;
        this._audioSource.Play();
    }

    public void PlayNormalClip()
    {
        this._audioSource.Stop();
        this._audioSource.volume /= 2;

        this._audioSource.clip = this._noiseClip;
        this._audioSource.Play();
    }
}
