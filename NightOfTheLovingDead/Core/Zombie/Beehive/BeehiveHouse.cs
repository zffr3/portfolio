using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeehiveHouse : MonoBehaviour, IInteractble
{
    [SerializeField]
    private GameObject _beehive;

    public void OnInteract(Collider sender)
    {
        this._beehive.SetActive(true);
        Destroy(this);
    }

    
}
