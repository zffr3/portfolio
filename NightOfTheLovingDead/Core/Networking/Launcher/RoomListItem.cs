using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    [SerializeField]
    private Text visualTxt;

    private RoomInfo _info;

    public Text VisualTxt { get => visualTxt; set => visualTxt = value; }

    public void SetInfo(RoomInfo info)
    {
        this._info = info;
        this.VisualTxt.text = info.Name;
    }

    public void JoinRoom()
    {
        MenuUi.Instance.ActivatePanel("LoadScreen");
    }
}
