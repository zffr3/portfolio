using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _roomNameText;

    private RoomInfo _roomInfo;

    public void SetUp(RoomInfo info)
    {
        this._roomNameText.text = info.Name;
        this._roomInfo = info;
    }

    public void OnClick()
    {
        NetworkLauncher.Instance.JoinRoom(this._roomInfo);
    }
}
