using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemperatureTraining : TrainingRoundBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!this._isTrainingEnded)
            {
                this._isTrainingEnded = true;
                StartCoroutine(StopTrainingWithDelay());
            }
        }     
    }
}
