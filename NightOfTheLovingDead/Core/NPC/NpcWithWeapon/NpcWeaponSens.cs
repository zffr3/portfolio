using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWeaponSens : MonoBehaviour
{
    [SerializeField]
    private NpcWeaponAi _linkedAi;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Human>() != null)
        {
            this._linkedAi.HandleHumanEnter(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Human>() != null)
        {
            this._linkedAi.HandleHumanExit(other.gameObject);
        }
    }
}
