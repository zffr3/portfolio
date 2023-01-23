using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    [SerializeField]
    private bool _start = false;
    [SerializeField]
    private AnimationCurve _curve;
    [SerializeField]
    private float _duration = 1f;

    // Update is called once per frame
    void Update()
    {
        if (this._start)
        {
            this._start = false;
            StartCoroutine(Shaking());
        }
    }

    public void StartShaking()
    {
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = this.transform.position,
            positionBeforeShake = this.transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < this._duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = this._curve.Evaluate(elapsedTime / this._duration);
            this.transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        this.transform.localPosition = positionBeforeShake;
    }
}
