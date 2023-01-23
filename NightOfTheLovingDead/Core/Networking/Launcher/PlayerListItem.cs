using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    private Player _data;

    [SerializeField]
    private TMP_Text visualData;

    public TMP_Text VisualData { get => visualData; set => visualData = value; }

    public void SetData(Player plr)
    {
        this._data = plr;
        this.VisualData.text = plr.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (this._data == otherPlayer)
            Destroy(this.gameObject);

        base.OnPlayerLeftRoom(otherPlayer);
    }

    public override void OnLeftRoom()
    {
        Destroy(this.gameObject);
        base.OnLeftRoom();
    }
}
