using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerList : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _playerListContent;

    [SerializeField]
    private GameObject _playerListItemPrefab;

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Instantiate(this._playerListItemPrefab, this._playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public override void OnJoinedRoom()
    {

        foreach (Transform item in this._playerListContent)
        {
            Destroy(item.gameObject);
        }

        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(this._playerListItemPrefab, this._playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }
}
