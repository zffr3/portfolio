using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleTraining : TrainingRoundBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !this._isTrainingEnded)
        {
            this._isTrainingEnded = true;
            StartCoroutine(StopTrainingWithDelay());
        }
    }
}
