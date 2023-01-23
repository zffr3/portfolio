using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField]
    private List<GraphNode> _nodes;

    [SerializeField]
    private Ghost _ghost;

    [SerializeField]
    private float _huntInterval;
    [SerializeField]
    private int _huntPercent;
    [SerializeField]
    private float _huntingTimeMin;
    [SerializeField]
    private float _huntingTimeMax;

    private bool _isHunting;

    private void Start()
    {
        this._ghost.SetStartNode(this._nodes[Random.Range(0, this._nodes.Count)]);

        this._isHunting = false;

        StartCoroutine(HuntInterval());
        StartCoroutine(HuntPercentBooster());

        EventBus.SubscribeToEvent(EventType.CURVEDITEM_TAKED, CurvedItemTaked);
        EventBus.SubscribeToEvent(EventType.NORMALITEM_TAKED, NormalItemTaked);
    }

    private void OnDisable()
    {
        EventBus.UnsubscribeFromEvent(EventType.CURVEDITEM_TAKED, CurvedItemTaked);
        EventBus.UnsubscribeFromEvent(EventType.NORMALITEM_TAKED, NormalItemTaked);
    }

    private void CurvedItemTaked(object sender, object param)
    {
        int seed = Random.Range(0, 100);

        if (seed <= 5)
        {
            StartHunt();
        }
    }

    private void NormalItemTaked(object sender, object param)
    {
        StartHunt();
    }

    private void StartHunt()
    {
        StartCoroutine(StartHunting());
    }

    private void HandleSensStateChanged(object sender, object param)
    {
        if (this._isHunting)
        {
            return;
        }

        if ((bool)param)
        {
            this._huntPercent *= 2;
        }
        else
        {
            this._huntPercent /= 2;
        }

        StopAllCoroutines();
        StartCoroutine(HuntInterval());
        StartCoroutine(HuntPercentBooster());
    }

    IEnumerator HuntInterval()
    {
        yield return new WaitForSeconds(this._huntInterval);

        int randomSeed = Random.Range(0, 100);
        if (randomSeed <= this._huntPercent)
        {
            if (!this._isHunting)
            {
                StartCoroutine(StartHunting());
            }
        }
        else
        {
            StartCoroutine(HuntInterval());

        }
    }

    IEnumerator HuntPercentBooster()
    {
        yield return new WaitForSeconds(60);

        this._huntPercent *= 2;

        StartCoroutine(HuntPercentBooster());
    }

    IEnumerator StartHunting()
    {
        this._isHunting = true;

        EventBus.Dispath(EventType.START_HUNTING, this,this);
        yield return new WaitForSeconds(Random.Range(this._huntingTimeMin, this._huntingTimeMax));
        EventBus.Dispath(EventType.STOP_HUNTING, this,this);

        this._isHunting = false;

        StartCoroutine(HuntInterval());
    }
}
