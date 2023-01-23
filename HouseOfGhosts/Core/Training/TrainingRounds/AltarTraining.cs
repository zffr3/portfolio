using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarTraining : TrainingRoundBase
{
    // Start is called before the first frame update
    void Start()
    {
        EventBus.SubscribeToEvent(EventType.CURVEDITEM_TAKED, HandleCursedItemTaked);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.CURVEDITEM_TAKED, HandleCursedItemTaked);
    }

    private void HandleCursedItemTaked(object param, object sender)
    {
        this._isTrainingEnded=true;
        StartCoroutine(StopTrainingWithDelay());
    }
}
