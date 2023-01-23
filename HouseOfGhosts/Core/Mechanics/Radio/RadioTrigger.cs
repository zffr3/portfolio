using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    [SerializeField]
    private Radio _linkedRadio;
    [SerializeField]
    private bool _acitvated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            if (!this._acitvated)
            {
                EventBus.Dispath(EventType.PLAYER_STATAE_CHANGED, this, PlayerState.NEAR_RADIO);
            }
        }

        if (other.GetComponent<Ghost>() != null || other.tag == "GhostBody")
        {
            if (this._acitvated)
            {
                this._linkedRadio.PlayDemonClip();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            if (!this._acitvated)
            {
                EventBus.Dispath(EventType.PLAYER_STATAE_CHANGED, this, PlayerState.DEFAULT);
            }
        }

        if (other.GetComponent<Ghost>() != null || other.tag == "GhostBody")
        {
            if (this._acitvated)
            {
                this._linkedRadio.PlayNormalClip();
            }
        }
    }

    public void ActivateRadio()
    {
        this._acitvated = true;
    }
}
