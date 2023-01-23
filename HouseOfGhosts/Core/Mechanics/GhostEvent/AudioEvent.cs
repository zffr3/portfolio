using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvent : MonoBehaviour
{
    [SerializeField]
    private AudioSource _aSource;

    private bool _canPlayeEvent;

    private void Start()
    {
        this._canPlayeEvent = true;
        EventBus.SubscribeToEvent(EventType.GHOSTEVENT_ACTIVATION_CHANGED, ResetEvent);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.GHOSTEVENT_ACTIVATION_CHANGED, ResetEvent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this._canPlayeEvent == false)
        {
            return;
        }

        if (other.GetComponent<Character>() != null)
        {
            this._aSource.Play();
            EventBus.Dispath(EventType.GHOSTEVENT_ACTIVATION_CHANGED, this, false);
        }
    }

    private void ResetEvent(object sender, object param)
    {
        this._canPlayeEvent = (bool)param;
    }
}
