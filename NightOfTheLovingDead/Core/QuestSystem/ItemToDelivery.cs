using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToDelivery : MonoBehaviour
{
    [SerializeField]
    private string _playerWhoStartedQuest;

    [SerializeField]
    private GameObject _merceTracker;
    [SerializeField]
    private bool _isTaked;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject _marcerObject;

    public void ConfigureItem(string plrName)
    {
        this._marcerObject.SetActive(true);
    }

    public string GetPlrName()
    {
        return this._playerWhoStartedQuest;
    }

    private void Update()
    {
        if (this._merceTracker == null && this._isTaked)
        {
            this._isTaked = false;
            this.GetComponent<BoxCollider>().isTrigger = true;
            this._marcerObject.SetActive(true);
        }
        if (this._isTaked)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this._merceTracker.transform.position + (Vector3.up * 2.5f), this._speed);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (this._merceTracker == null)
        {
            Human player = collision.gameObject.GetComponent<Human>();

            if (player != null)
            {
                this._isTaked = true;
                this._merceTracker = collision.gameObject;
                this._marcerObject.SetActive(false);
            }
        }
    }
}
