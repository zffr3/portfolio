using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEventController : MonoBehaviour
{
    [SerializeField]
    private float _eventActivationTime;

    [SerializeField]
    private List<GameObject> _disabledEvents;


    [SerializeField]
    private bool _canActivateEvent;

    // Start is called before the first frame update
    void Start()
    {
        this._canActivateEvent = true;

        EventBus.Dispath(EventType.GHOSTEVENT_ACTIVATION_CHANGED, this, this._canActivateEvent);

        EventBus.SubscribeToEvent(EventType.GHOSTEVENT_ACTIVATION_CHANGED, RestEventActivation);
    }

    private void OnDisable()
    {
        EventBus.UnsubscribeFromEvent(EventType.GHOSTEVENT_ACTIVATION_CHANGED, RestEventActivation);
    }

    private void RestEventActivation(object sender, object param)
    {
        if ((bool)param)
        {
            return;
        }

        StartCoroutine(ActivateEvent());
    }

    private IEnumerator ActivateEvent()
    {
        yield return new WaitForSeconds(this._eventActivationTime);

        this._canActivateEvent = true;
        EventBus.Dispath(EventType.GHOSTEVENT_ACTIVATION_CHANGED, this, this._canActivateEvent);
    }
}
