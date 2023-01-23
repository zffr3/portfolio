using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightController : MonoBehaviour
{
    private GameObject _building;
    [SerializeField]
    private int _fragmentCount;
    [SerializeField]
    private int _demolishedFragmentsCount;

    [SerializeField]
    private bool[] _stars;

    private void Awake()
    {
        EventBus.SubscribeToEvent(EventType.BUILDING_INITIALIZED, HandleInitializeBuilding);
        EventBus.SubscribeToEvent(EventType.LEVEL_CLOSED, CalculateStars);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.BUILDING_INITIALIZED, HandleInitializeBuilding);
        EventBus.UnsubscribeFromEvent(EventType.LEVEL_CLOSED, CalculateStars);
    }

    public void FragmentDemolished()
    {
        this._demolishedFragmentsCount--;

        if (this._fragmentCount * 0.65 > this._demolishedFragmentsCount)
        {
            if (!this._stars[0])
            {
                this._stars[0] = true;
                EventBus.Dispath(EventType.STAR_TAKED, this, 0);
            }
        }

        if (this._fragmentCount * 0.45 > this._demolishedFragmentsCount)
        {
            if (!this._stars[1])
            {
                this._stars[1] = true;
                EventBus.Dispath(EventType.STAR_TAKED, this, 1);
            }
        }

        if (this._fragmentCount * 0.3 > this._demolishedFragmentsCount)
        {
            if (!this._stars[2])
            {
                this._stars[2] = true;
                EventBus.Dispath(EventType.STAR_TAKED, this, 2);

                Destroy(this);
            }
        }
    }

    private void HandleInitializeBuilding(object sender, object param)
    {
        this._fragmentCount = (int)param;

        this._demolishedFragmentsCount = this._fragmentCount;
    }

    private void CalculateStars(object sender, object param)
    {
        int takedStarCount = 0;
        for (int i = 0; i < this._stars.Length; i++)
        {
            if (this._stars[i])
            {
                takedStarCount++;
            }
        }

        EventBus.Dispath(EventType.STARS_GIVEN_COMPLETE, this, takedStarCount);
    }
}
