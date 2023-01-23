using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShotCounter : MonoBehaviour
{
    [SerializeField]
    private int _shotCounter;

    private bool _eventCalled;

    // Start is called before the first frame update
    void Awake()
    {
        EventBus.SubscribeToEvent(EventType.SHOT_BTN_PRESSED, HandleShot);
        this._eventCalled = false;
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.SHOT_BTN_PRESSED, HandleShot);
    }

    private void HandleShot(object sender, object param)
    {
        this._shotCounter++;
        DetermineShotCount();   
    }

    private void DetermineShotCount()
    {
        if (this._shotCounter >= 10 && !this._eventCalled)
        {
            this._eventCalled = true;
            EventBus.Dispath(EventType.BULLETS_ENDED, this, this);
        }
    } 
}
