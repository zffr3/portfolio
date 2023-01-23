using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _roonListContent;

    [SerializeField]
    private GameObject _roonListPrefab;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform item in this._roonListContent)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                continue;
            }

            Instantiate(this._roonListPrefab, this._roonListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
}
