using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _shotSoundSource;
    [SerializeField]
    private AudioSource _starTakedSource;
    [SerializeField]
    private AudioSource _levelEndSource;
    [SerializeField]
    private AudioSource _concreteSource;

    [SerializeField]
    private List<AudioClip> _concreteSmashClips;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.SubscribeToEvent(EventType.SHOT_BTN_PRESSED, PlayShotSound);
        EventBus.SubscribeToEvent(EventType.STAR_TAKED, PlayStarSound);
        EventBus.SubscribeToEvent(EventType.BULLETS_FRAGMENT_TOUCHED, PlayConcreteSmashSound);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.SHOT_BTN_PRESSED, PlayShotSound);
        EventBus.UnsubscribeFromEvent(EventType.STAR_TAKED, PlayStarSound);
        EventBus.UnsubscribeFromEvent(EventType.BULLETS_FRAGMENT_TOUCHED, PlayConcreteSmashSound);
    }

    private void PlayShotSound(object sender, object param)
    {
        if (this._shotSoundSource != null)
        {
            this._shotSoundSource.Play();
        }
    }

    private void PlayStarSound(object sender, object param)
    {
        if (this._starTakedSource != null && this._levelEndSource != null)
        {
            int starCount = (int)param;
            if (starCount != 2)
            {
                this._starTakedSource.Play();
            }
            else
            {
                this._levelEndSource.Play();
            }
        }
    }

    private void PlayConcreteSmashSound(object sender, object param)
    {
        this._concreteSource.clip = this._concreteSmashClips[Random.Range(0, this._concreteSmashClips.Count)];
        this._concreteSource.Play();    
    }
}
