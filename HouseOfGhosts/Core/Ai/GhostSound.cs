using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSound : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip[] _screams;

    [SerializeField]
    private AudioClip _metalSound;

    private float _cdScream;
    private bool _canPlaySound;
    private bool _canMetalDrop;

    private void Start()
    {
        this._canPlaySound = true;
        this._audioSource = this.GetComponent<AudioSource>();

        StopCoroutine(PlayDropMetalSound());
    }

    public void Scream()
    {
        this._canMetalDrop = false;
        if (this._canPlaySound) 
        {
            this._audioSource.Stop();
            AudioClip screamClip = this._screams[Random.Range(0, this._screams.Length)];

            this._canPlaySound = false;
            this._audioSource.clip = screamClip;
            this._audioSource.Play();

            this._cdScream = screamClip.length;

            StartCoroutine(ResetScream());
        }
    }

    public void AllowMetalDrop()
    {
        this._canMetalDrop = true;
    }

    IEnumerator ResetScream()
    {
        yield return new WaitForSecondsRealtime(this._cdScream + Random.Range(2f, 5f));
        this._canPlaySound = true;
    }

    IEnumerator PlayDropMetalSound()
    {
        float timeCd = Random.Range(5f, 20f);
        Debug.Log(timeCd);
        yield return new WaitForSecondsRealtime(timeCd);
        if (this._canMetalDrop)
        {
            this._audioSource.clip = this._metalSound;
            this._audioSource.Play();
        }

        StartCoroutine(PlayDropMetalSound());
    }
}
