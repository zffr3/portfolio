using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedItem : MonoBehaviour
{
    [SerializeField]
    private GameObject _linkedNormalItem;

    [SerializeField]
    private Outline _outlineSrc;

    void Start()
    {
        this._outlineSrc = this.GetComponent<Outline>();
        EventBus.SubscribeToEvent(EventType.CANDLE_ACTIVATED, OnCandleActivated);
        EventBus.SubscribeToEvent(EventType.CANDLE_DIACTIVATED, OnCandleDiactivated);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.CANDLE_ACTIVATED, OnCandleActivated);
        EventBus.UnsubscribeFromEvent(EventType.CANDLE_DIACTIVATED, OnCandleDiactivated);
    }
    private void OnCandleActivated(object sender, object param)
    {
        this._outlineSrc.enabled = true;
    }

    private void OnCandleDiactivated(object sender, object param)
    {
        this._outlineSrc.enabled = false;
    }

    public GameObject GetNormalItem()
    {
        return this._linkedNormalItem;
    }
}
