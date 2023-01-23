using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureZone : MonoBehaviour
{
    [SerializeField]
    private bool _ghostInZone;
    [SerializeField]
    private bool _playerInZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ghost>() != null)
        {
            this._ghostInZone = true;

            if (this._playerInZone)
            {
                TemperatureController.instance.GhostEnter();
            }
        }

        if (other.GetComponent<Character>() != null)
        {
            this._playerInZone = true;
            TemperatureController.instance.ChangeTemperatureZone(this._ghostInZone);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Ghost>() != null)
        {
            this._ghostInZone = false;
            if (this._playerInZone)
            {
                TemperatureController.instance.GhostExit();
            }
        }

        if (other.GetComponent<Character>() != null)
        {
            this._playerInZone = false;
        }
    }
}
