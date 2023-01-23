using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkMenuUi : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _startButton;

    public override void OnJoinedRoom()
    {
        this._startButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        this._startButton.SetActive(PhotonNetwork.IsMasterClient);
    }
}
