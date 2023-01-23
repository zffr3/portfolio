using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTriggerEvent : GhostEventBase
{
    [SerializeField]
    private GameObject _eventObject;

    private bool _camPlayeEvent;

    private void Start()
    {
        this._camPlayeEvent = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            if (this._camPlayeEvent == false)
            {
                return;
            }

            this._camPlayeEvent = false;
            this._eventObject.SetActive(true);

            base.ShakePlayerCamera(other);

            StartCoroutine(CloseEvent());
        }
    }

    IEnumerator CloseEvent()
    {
        yield return new WaitForSeconds(5f);

        this._camPlayeEvent = true;
        this._eventObject.SetActive(false);
    }
}
