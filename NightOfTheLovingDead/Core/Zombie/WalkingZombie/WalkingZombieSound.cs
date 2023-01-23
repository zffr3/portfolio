using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingZombieSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] _zombieSounds;

    [SerializeField]
    private AudioSource _zombieAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        this._zombieAudioSource = GetComponent<AudioSource>();
        StartCoroutine(ChangeAudioClip());
    }

    IEnumerator ChangeAudioClip()
    {
        int seed = Random.Range(0, 100);

        if (seed > this._zombieSounds.Length - 1)
        {
            yield return new WaitForSeconds(1f);
        }
        else
        {
            AudioClip selectedClip = this._zombieSounds[Mathf.Clamp(seed, 0, this._zombieSounds.Length)];
            this._zombieAudioSource.clip = selectedClip;

            this._zombieAudioSource.Play();

            yield return new WaitForSeconds(selectedClip.length);
        }
        StartCoroutine(ChangeAudioClip());
    }
}
