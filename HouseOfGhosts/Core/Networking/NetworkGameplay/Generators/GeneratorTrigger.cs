using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTrigger : MonoBehaviour
{
    [SerializeField]
    private Generator _linkedGenerator;

    private bool _interacted;

    private void Awake()
    {
        if (this._linkedGenerator == null)
        {
            this._linkedGenerator = this.GetComponentInParent<Generator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                EventBus.SubscribeToEvent(EventType.INTERACT_BUTTON_VALUE, ReciveInput);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                if (this._interacted)
                {
                    this._linkedGenerator.StartActivation();
                }
                else
                {
                    this._linkedGenerator.StopActivation();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                EventBus.UnsubscribeFromEvent(EventType.INTERACT_BUTTON_VALUE, ReciveInput);
            }
        }
    }

    private void ReciveInput(object sender, object param)
    {
        this._interacted = (bool)param;
    }
}
