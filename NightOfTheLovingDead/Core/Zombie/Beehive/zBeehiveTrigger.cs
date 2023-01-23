using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zBeehiveTrigger : MonoBehaviour
{
    [SerializeField]
    private ZombieBeehive _linkedBeehive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() != null)
            this._linkedBeehive.AcceptPlayerToBeehive(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() != null)
            this._linkedBeehive.RaisPlayerFromBeehive(other.gameObject);
    }
}
