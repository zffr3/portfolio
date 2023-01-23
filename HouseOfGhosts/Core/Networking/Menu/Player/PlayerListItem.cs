using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    private Photon.Realtime.Player _playerData;

    [SerializeField]
    private TMP_Text _playerNameText;

    public void SetUp(Photon.Realtime.Player playerData)
    {
        this._playerData = playerData;
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        if (this._playerData == otherPlayer)
        {
            Destroy(this.gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(this.gameObject);
    }
}
