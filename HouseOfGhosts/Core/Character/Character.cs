using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private CursedRoomMarkers _marker;

    private void Start()
    {
        this._marker = CursedRoomMarkers.None;
    }

    public bool KillPlayer()
    {
        Destroy(this.gameObject);
        return true;
    }

    public void ActivateMarker(CursedRoomMarkers marker)
    {
        this._marker = marker;
    }

    public CursedRoomMarkers GetCurrentMarker()
    {
        return this._marker;
    }
}
