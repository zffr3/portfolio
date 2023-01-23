using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class McComponentManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private List<MonoBehaviour> _masterClientComponents;

    [SerializeField]
    private List<GameObject> _masterClientObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            DisableComponents();
            DisableObjects();
        }
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            DisableComponents();
            DisableObjects();
        }
    }

    private void DisableComponents()
    {
        if (this._masterClientComponents != null && this._masterClientComponents.Count != 0)
        {
            for (int i = 0; i < this._masterClientComponents.Count; i++)
            {
                this._masterClientComponents[i].enabled = false;
            }
        }
    }

    private void DisableObjects()
    {
        if (this._masterClientObjects != null && this._masterClientObjects.Count != 0)
        {
            for (int i = 0; i < this._masterClientObjects.Count; i++)
            {
                this._masterClientObjects[i].SetActive(false);
            }
        }
    }
}
