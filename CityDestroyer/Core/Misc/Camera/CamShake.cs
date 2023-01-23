using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve _curve;
    [SerializeField]
    private float _duration = 1f;

    private void Start()
    {
        EventBus.SubscribeToEvent(EventType.BULLET_COLISTION_ENTER, StartShaking);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.BULLET_COLISTION_ENTER, StartShaking);
    }


    private void StartShaking(object sender, object param)
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
