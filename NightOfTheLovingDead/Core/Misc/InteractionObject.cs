using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    private IInteractble _interactbleSrc;

    private bool _isInteractable;

    // Start is called before the first frame update
    void Start()
    {
        this._interactbleSrc = this.GetComponent<IInteractble>();
        this._isInteractable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() != null && this._isInteractable)
        {
            PlayerUI.Instance.ChangeNpcInteractionPanelActiveState(true);
            if (PlayerKeys.Instance.Interact && this._interactbleSrc != null)
            {
                this._interactbleSrc.OnInteract(other);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() != null && this._isInteractable)
        {  
            if (PlayerKeys.Instance.Interact && this._interactbleSrc != null)
            {
                this._interactbleSrc.OnInteract(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() != null)
        {
            PlayerUI.Instance.ChangeNpcInteractionPanelActiveState(false);
        }
    }

    public void StopInteraction()
    {
        this._isInteractable = false;
    }

    public void ContinueInteraction()
    {
        this._isInteractable = true;
    }

}

interface IInteractble
{
     void OnInteract(Collider sender);
}
