using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GeneratorController : MonoBehaviour
{
    public static GeneratorController Instance { get; private set; }

    [SerializeField]
    private PhotonView _pView;

    [SerializeField]
    private bool[] _generatorsActivationStatus;

    [SerializeField]
    private GameObject _door;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        this._generatorsActivationStatus = new bool[3];
    }

    public void ActivateGenerator(int generatorId)
    {
        this._generatorsActivationStatus[generatorId] = true;
        CheckGeneratorStatus();
    }


    private void CheckGeneratorStatus()
    {
        bool allActivated = this._generatorsActivationStatus[0];

        for (int i = 1; i < this._generatorsActivationStatus.Length; i++)
        {
            allActivated = allActivated & this._generatorsActivationStatus[i];
        }

        if (allActivated)
        {
            this._pView.RPC(nameof(RPC_HandleGeneratorActivated), RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_HandleGeneratorActivated()
    {
        this._door.SetActive(false);
    }
}
