using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightTrigger : MonoBehaviour
{
    [SerializeField]
    private HeightController _linkedController;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fragment")
        {
            this._linkedController.FragmentDemolished();
        }
    }
}
