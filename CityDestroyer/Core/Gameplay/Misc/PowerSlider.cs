using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSlider : MonoBehaviour
{
    [SerializeField]
    private Slider _powerSlider;

    private bool _plus;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SlidePower());
        this._plus = true;
    }

    public void StopSliding()
    {
        StopCoroutine(SlidePower());
    }

    private IEnumerator SlidePower()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        if (this._powerSlider.value == 10)
        {
            this._plus = false;
        }
        if (this._powerSlider.value == 1)
        {
            this._plus = true;
        }

        if (this._plus)
        {
            this._powerSlider.value += 1;
        }
        else
        {
            this._powerSlider.value -= 1;
        }

        StartCoroutine(SlidePower());
    }
}
