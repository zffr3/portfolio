using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class KeysController : MonoBehaviour
{
    [SerializeField]
    private PhotonView _pView;

    [SerializeField]
    private int _keyCount;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GenerateKeys();
        }
    }

    private void GenerateKeys()
    {
        for (int i = 0; i < this._keyCount; i++)
        {
            Transform spawnPoint = SimpleTransformPool.Instance.GetRandomPoint(SimplePoolTypes.Keys);

            GameObject keyInstance = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Key"), spawnPoint.position, spawnPoint.rotation);
            keyInstance.GetComponent<NetKey>().ConfigureKey(i);
        }
    }

    public void TakeKey()
    {

    }
}
