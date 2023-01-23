using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerTraining : TrainingRoundBase
{
    [SerializeField]
    private GameObject _locker;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.SubscribeToEvent(EventType.RELEASE_PLAYER, HandleReleasePlayer);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.RELEASE_PLAYER, HandleReleasePlayer);
    }

    private void HandleReleasePlayer(object param, object sender)
    {
        this._locker.SetActive(false);

        this._isTrainingEnded = true;
        StartCoroutine(StopTrainingWithDelay());
    }
}
