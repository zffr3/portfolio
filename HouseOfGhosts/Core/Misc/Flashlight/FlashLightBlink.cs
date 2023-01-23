using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightBlink : MonoBehaviour
{
    [SerializeField]
    private Animator _flashlightAnim;

    private bool _isHunting;

    private void Start()
    {
        EventBus.SubscribeToEvent(EventType.START_HUNTING, HandleStartHunting);
        EventBus.SubscribeToEvent(EventType.STOP_HUNTING, StopStartHunting);
    }

    private void OnEnable()
    {
        this._flashlightAnim.SetBool("Blink", this._isHunting);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.START_HUNTING, HandleStartHunting);
        EventBus.UnsubscribeFromEvent(EventType.STOP_HUNTING, StopStartHunting);
    }

    public void StartBlinking()
    {
        this._flashlightAnim.SetBool("Blink", true);
    }

    public void StopBlinking()
    {
        if (!this._isHunting)
        {
            this._flashlightAnim.SetBool("Blink", false);
        }
    }

    private void HandleStartHunting(object sender, object param)
    {
        this._isHunting = true;
        StartBlinking();
    }

    private void StopStartHunting(object sender, object param)
    {
        this._isHunting = false;
        StopBlinking();
    }
}
