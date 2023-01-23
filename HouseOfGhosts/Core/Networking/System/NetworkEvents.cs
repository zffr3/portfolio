using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkEvents : MonoBehaviourPunCallbacks
{
    public static NetworkEvents Instance { get; private set; }

    [SerializeField]
    private PhotonView _pView;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (this._pView == null)
        {
            this._pView = this.GetComponent<PhotonView>();
        }
    }

    public void CallRpcEvent(EventType type, object sender, object param)
    {
        this._pView.RPC(nameof(DispatchRPC), RpcTarget.Others, type, sender, param);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
    }

    [PunRPC]
    private void DispatchRPC(int eventType,object sender, object param)
    {
        EventBus.Dispath((EventType)eventType, sender, param);
    }
}
