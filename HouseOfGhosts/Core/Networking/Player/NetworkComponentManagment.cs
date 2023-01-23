using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkComponentManagment : MonoBehaviour
{
    [SerializeField]
    private PhotonView _pView;

    [SerializeField]
    private List<MonoBehaviour> _selfRootComponents;
    [SerializeField]
    private List<GameObject> _selfRootObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (this._pView == null)
        {
            this._pView.GetComponent<PhotonView>();
        }

        if (!this._pView.IsMine)
        {
            DisableComponents();
            DisableObjects();
        }

    }

    private void DisableComponents()
    {
        if (this._selfRootComponents != null && this._selfRootComponents.Count != 0)
        {
            for (int i = 0; i < this._selfRootComponents.Count; i++)
            {
                this._selfRootComponents[i].enabled = false;
            }
        }
    }

    private void DisableObjects()
    {
        if (this._selfRootObjects != null && this._selfRootObjects.Count != 0)
        {
            for (int i = 0; i < this._selfRootObjects.Count; i++)
            {
                this._selfRootObjects[i].SetActive(false);
            }
        }
    }
}
