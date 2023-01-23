using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmfTraining : TrainingRoundBase
{
    [SerializeField]
    private List<GameObject> _triningObjects;

    private bool _trainingStoped;

    // Start is called before the first frame update
    void Start()
    {
        this._trainingStoped = false;

        EventBus.SubscribeToEvent(EventType.TRAININGSTATE_CHANGED, StartTraining);
        EventBus.SubscribeToEvent(EventType.EMF_HIGHLITED, StopTraining);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.TRAININGSTATE_CHANGED, StartTraining);
        EventBus.UnsubscribeFromEvent(EventType.EMF_HIGHLITED, StopTraining);
    }

    private void StartTraining(object sender, object param)
    {
        if ((TrainingStates)param == TrainingStates.Emf)
        {
            TrainingUi.Instance.DisplayMessage(this._localizationSrc.GetWord(this._startText));
            for (int i = 0; i < this._triningObjects.Count; i++)
            {
                this._triningObjects[i].SetActive(true);
            }
        }
    }

    private void StopTraining(object sender, object param)
    {
        if (!this._trainingStoped)
        {
            this._trainingStoped = true;
            StartCoroutine(StopTrainingWithDelay());
        }
    }
}
