using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTraining : TrainingRoundBase
{
    [SerializeField]
    private GameObject _playerSrc;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private GameObject _nunObject;

    // Start is called before the first frame update
    void Start()
    {
        this._playerSrc.GetComponent<CamRaycaster>().enabled = true;

        EventBus.SubscribeToEvent(EventType.RADIO_ACTIVATED, HandleRadioActivated);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.RADIO_ACTIVATED, HandleRadioActivated);
    }

    private void HandleRadioActivated(object sender, object param)
    {
        this._nunObject.SetActive(true);
        this._animator.SetTrigger("Play");
    }

    public void HandleEndOfAnimation()
    {
        this._isTrainingEnded = true;
        StartCoroutine(StopTrainingWithDelay());
    }
}
