using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyEvent : GhostEventBase
{
    [SerializeField]
    private Rigidbody _sourceBody;

    private bool _complete;

    [SerializeField]
    private bool _adForce;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private Transform _eventObject;

    private bool _canPlayeEvent;

    private void Start()
    {
        this._complete = false;
        this._audioSource = GetComponent<AudioSource>();
        EventBus.SubscribeToEvent(EventType.GHOSTEVENT_ACTIVATION_CHANGED, ResetEvent);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.GHOSTEVENT_ACTIVATION_CHANGED, ResetEvent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!this._canPlayeEvent)
        {
            return;
        }

        if (other.GetComponent<Character>() != null)
        {
            if (this.CheckPlayerAngle(this._eventObject, other.gameObject.transform))
            {
                if (!this._complete)
                {
                    this._sourceBody.useGravity = true;
                    this._complete = true;
                    this._audioSource.Play();
                    if (this._adForce)
                    {
                        this._sourceBody.AddForce(this.transform.forward, ForceMode.Impulse);
                    }
                    EventBus.Dispath(EventType.GHOSTEVENT_ACTIVATION_CHANGED, this, false);
                }
            }

        }
    }

    private void ResetEvent(object sender, object param)
    {
        this._canPlayeEvent = (bool)param;
    }
}
