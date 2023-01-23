using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField]
    private PhotonView _pView;

    // Start is called before the first frame update
    void Start()
    {
        this._pView = this.GetComponent<PhotonView>();

        if (this._pView.IsMine)
        {
            CreateCharacter();
        }
    }

    private void CreateCharacter()
    {
        Transform spawnPoint = SimpleTransformPool.Instance.GetRandomPoint(SimplePoolTypes.Players);

        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "FPControllerNetwork"), spawnPoint.position, spawnPoint.rotation);
    }
}
