using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEventAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource _knockKnockudioSource;
    [SerializeField]
    private AudioSource _lockerKnockAudioSource;
    [SerializeField]
    private AudioSource _metalImpactAudioSource;
    [SerializeField]
    private AudioSource _screamAudioSource;

    public void PlayKnock()
    {
        this._knockKnockudioSource.Play();
    }

    public void PlayLocker()
    {
        this._lockerKnockAudioSource.Play();
    }

    public void PlayMetalImpact()
    {
        this._metalImpactAudioSource.Play();
    }

    public void PlayScream()
    {
        this._screamAudioSource.Play();
    }
}
