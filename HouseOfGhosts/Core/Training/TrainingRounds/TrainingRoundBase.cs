using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingRoundBase : MonoBehaviour
{
    [SerializeField]
    protected TrainingSystem _training;

    [SerializeField]
    protected LocalizationSystem _localizationSrc;

    [SerializeField]
    protected LanguageKey _startText;
    [SerializeField]
    protected LanguageKey _endText;

    protected bool _isTrainingEnded;

    void Start()
    {
        TrainingUi.Instance.DisplayMessage(this._localizationSrc.GetWord(this._startText));
        this._isTrainingEnded = false;
    }

    protected IEnumerator StopTrainingWithDelay()
    {
        this._isTrainingEnded = true;
        TrainingUi.Instance.DisplayMessageAndClear(this._localizationSrc.GetWord(this._endText));
        yield return new WaitForSecondsRealtime(5);

        this._training.StartNextRound();
    }
}
