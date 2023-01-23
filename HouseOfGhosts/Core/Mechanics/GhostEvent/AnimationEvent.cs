using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : GhostEventBase
{
    [SerializeField]
    private Animator _animSource;

    [SerializeField]
    private string _animationKey;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private Transform _eventObject;

    [SerializeField]
    private bool _useGlobalSound;

    private bool _canPlayeEvent;

    private FlashLightBlink _blink;

    private bool _isHunting;

    // Start is called before the first frame update
    void Start()
    {
        this._canPlayeEvent = true;

        EventBus.SubscribeToEvent(EventType.START_HUNTING, HandleStartHunting);
        EventBus.SubscribeToEvent(EventType.STOP_HUNTING, HandleStopHunting);
        EventBus.SubscribeToEvent(EventType.GHOSTEVENT_ACTIVATION_CHANGED, ResetEvent);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.START_HUNTING, HandleStartHunting);
        EventBus.UnsubscribeFromEvent(EventType.STOP_HUNTING, HandleStopHunting);
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
            if (this.CheckPlayerAngle(this._eventObject, other.gameObject.transform))
            {
                StartCoroutine(DisableBlink());
                this._blink = other.GetComponent<FlashLightBlink>();
                this._blink?.StartBlinking();

                this._canPlayeEvent = false;
                this._animSource.SetTrigger(this._animationKey);

                if (this._useGlobalSound)
                {
                    this._audioSource?.Play();
                }

                this.gameObject.RemoveComponent<AnimationEvent>();
                EventBus.Dispath(EventType.GHOSTEVENT_ACTIVATION_CHANGED, this, false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this._canPlayeEvent == false)
        {
            return;
        }

        if (other.GetComponent<Character>() != null && !this._isHunting)
        {
            StopBlinking();
        }
    }

    public void StopBlinking()
    {
        if (this._blink != null)
        {
            this._blink.StopBlinking();
        }
    }

    private void HandleStartHunting(object sender, object param)
    {
        this._isHunting = true;
    }

    private void HandleStopHunting(object sender, object param)
    {
        this._isHunting= false;
    }

    private void ResetEvent(object sender, object param)
    {
        this._canPlayeEvent = (bool)param;
    }

    IEnumerator DisableBlink()
    {
        yield return new WaitForSeconds(3f);
        StopBlinking();
    }
}
