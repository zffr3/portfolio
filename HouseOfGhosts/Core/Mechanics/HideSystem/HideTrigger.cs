using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTrigger : MonoBehaviour
{
    [SerializeField]
    private HideSystem _linkedSystem;

    private bool _hiden;

    private bool _playerInTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null && !this._hiden)
        {
            this._playerInTrigger = true;
            EventBus.Dispath(EventType.PLAYER_STATAE_CHANGED, this, PlayerState.NEAR_LOKER);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>() != null && !this._hiden)
        {
            this._playerInTrigger = false;                                                                                            
            EventBus.Dispath(EventType.PLAYER_STATAE_CHANGED, this, PlayerState.DEFAULT);
        }
    }

    public void ChangeHidenState(bool isHiden)
    {
        this._hiden = isHiden;
    }

    public bool GetPlayerStatus()
    {
        return this._playerInTrigger;
    }
}
