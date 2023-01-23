using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSens : MonoBehaviour
{
    [SerializeField]
    private bool _isHaveTarget;

    public event System.Action<GameObject> FindTarget;
    public event System.Action LostPlayer;

    // Start is called before the first frame update
    void Start()
    {
        this._isHaveTarget = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Human hSrc = other.GetComponent<Human>();
        if (hSrc != null && hSrc.CurrentType != HumanType.NPC && !this._isHaveTarget)
        {
            this._isHaveTarget = true;
            this.FindTarget?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Human hSrc = other.GetComponent<Human>();
        if (hSrc != null && hSrc.CurrentType != HumanType.NPC && !this._isHaveTarget)
        {
            this._isHaveTarget = false;
            this.LostPlayer?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Human hSrc = other.GetComponent<Human>();
        if (hSrc != null && hSrc.CurrentType != HumanType.NPC && !this._isHaveTarget)
        {
            this._isHaveTarget = true;
            this.FindTarget?.Invoke(other.gameObject);
        }
    }

    public void SetTargetState(bool state)
    {
        this._isHaveTarget = state;
    }

}
