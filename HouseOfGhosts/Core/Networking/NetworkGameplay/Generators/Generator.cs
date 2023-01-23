using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private int _generatorId;

    [SerializeField]
    private PhotonView _pView;

    [SerializeField]
    private int _generatorActivasionPercent;

    [SerializeField]
    private bool _activationInProgress;
    [SerializeField]
    private bool _isActivated;

    public void StartActivation()
    {
        this._activationInProgress = true;
        StartCoroutine(Activation());
    }

    public void StopActivation()
    {
        this._activationInProgress = false;
    }

    IEnumerator Activation()
    {
        yield return new WaitForSecondsRealtime(2f);
        if (this._activationInProgress && !this._isActivated)
        {
            this._pView.RPC(nameof(RPC_ActivateGenerator), RpcTarget.All);

            if (this._generatorActivasionPercent >= 100)
            {
                this._isActivated = true;
            }
            else
            {
                StartCoroutine(Activation());
            }
        }
    }

    [PunRPC]
    private void RPC_ActivateGenerator()
    {
        this._generatorActivasionPercent += 1;

        if (this._generatorActivasionPercent >= 100)
        {
            this._pView.RPC(nameof(RPC_GeneratorActivated), RpcTarget.MasterClient, this._generatorId);
            this.enabled = false;
        }
    }

    [PunRPC]
    private void RPC_GeneratorActivated(int generatorId)
    {
        GeneratorController.Instance.ActivateGenerator(generatorId);
    }
}
