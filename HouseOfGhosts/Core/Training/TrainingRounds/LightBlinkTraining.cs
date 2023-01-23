using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlinkTraining : TrainingRoundBase
{
    [SerializeField]
    private FlashLightBlink _blinkSrc;

    // Start is called before the first frame update
    void Start()
    {
        this._blinkSrc.StartBlinking();
        StartCoroutine(EndTraining());
    }

    IEnumerator EndTraining()
    {
        StartCoroutine(StopTrainingWithDelay());
        yield return new WaitForSecondsRealtime(2.5f);

        this._blinkSrc.StopBlinking();
    }
}
