using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetKey : MonoBehaviour
{
    [SerializeField]
    private PhotonView _pView;

    private int _keyId;
    private bool _taked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !this._taked)
        {
            TakeKey();
        }
    }

    public void ConfigureKey(int keyId)
    {
        if (this._pView == null)
        {
            this._pView = this.GetComponent<PhotonView>();
        }

        this._keyId = keyId;
        this._pView.RPC(nameof(RPC_SyncKeyID), RpcTarget.Others, keyId);
    }

    private void TakeKey()
    {
        this._taked = true;
    }


    [PunRPC]
    private void RPC_SyncKeyID(int keyId)
    {
        this._keyId = keyId;
    }

    [PunRPC]
    private void RPC_ActivateKey()
    {
        EventBus.Dispath(EventType.NET_KEY_ACTIVATED, this, this._keyId);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
