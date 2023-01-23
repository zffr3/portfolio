using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTeleport : MonoBehaviour
{
    [SerializeField]
    private Transform _teleportTarget;

    [SerializeField]
    private int _teleportCost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() != null)
        {
            PlayerUI.Instance.ChangeNpcInteractionPanelActiveState(true);
            if (PlayerKeys.Instance.Interact)
                if (other.GetComponent<NetworkCharacter>().DiscrabXp(this._teleportCost))
                    NetworkPlayer.NetworkPlayerInstance.TeleportController(this._teleportTarget.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() != null)
        {
            PlayerUI.Instance.ChangeNpcInteractionPanelActiveState(true);
            if (PlayerKeys.Instance.Interact)
                if (other.GetComponent<NetworkCharacter>().DiscrabXp(this._teleportCost))
                    NetworkPlayer.NetworkPlayerInstance.TeleportController(this._teleportTarget.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() != null)
            PlayerUI.Instance.ChangeNpcInteractionPanelActiveState(false);
    }
}
