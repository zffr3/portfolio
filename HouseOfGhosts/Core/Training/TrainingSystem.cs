using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingSystem : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _trainingRounds;
    [SerializeField]
    private int _roundInd;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.SubscribeToEvent(EventType.TRAININGCAM_ITEMFINDED, HandleItemTrainingChanged);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.TRAININGCAM_ITEMFINDED, HandleItemTrainingChanged);
    }

    private void HandleItemTrainingChanged(object sender, object param)
    {
        TrainingStates state = (TrainingStates)param;

        if (state == TrainingStates.Emf || state == TrainingStates.Candle || state == TrainingStates.Thermometr)
        {
            EventBus.Dispath(EventType.TRAININGSTATE_CHANGED, this, state);
        }
    }

    public void StartNextRound()
    {
        this._trainingRounds[this._roundInd].gameObject.SetActive(false);
        this._roundInd++;

        this._trainingRounds[this._roundInd].gameObject.SetActive(true);

        EventBus.Dispath(EventType.TRAININGSTATE_CHANGED, this, (TrainingStates)this._roundInd);
    }
}
public enum TrainingStates
{
    Emf,
    Thermometr,
    Candle,
    Altar,
    Radio,
    Locker
}
