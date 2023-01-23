using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMap : MonoBehaviour
{
    public static GlobalMap Instance;

    [SerializeField]
    private GameObject _mapCamera;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void SubscribeToEventsFromPlayerKeys()
    {
        PlayerKeys.Instance._mapBtnPressed += ChangeMapState;
    }

    public void UnsubscribeToEventsFromPlayerKeys()
    {

        PlayerKeys.Instance._mapBtnPressed -= ChangeMapState;
    }

    public void ChangeMapState(bool mapState)
    {
        this._mapCamera.SetActive(mapState);
        PlayerUI.Instance.ChangeMapState(mapState);
        NetworkPlayer.NetworkPlayerInstance.CallChangeMapState(mapState);
    }
}
