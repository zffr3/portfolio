using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticNpc : MonoBehaviour, INpc
{
    [SerializeField]
    private List<GameObject> _skins;

    [SerializeField]
    private Settlement _settlementSrc;

    public event System.Action<GameObject> OnNpcDie;

    public void Configure(Transform target, Settlement src, System.Action<GameObject> dieHandler)
    {
        this._settlementSrc = src;
        this.OnNpcDie += dieHandler;
    }

    public void Die()
    {
        this.OnNpcDie?.Invoke(this.gameObject);
        this.OnNpcDie = null;
    }

    private void Start()
    {
        this._skins[Random.Range(0,this._skins.Count)].SetActive(true);
    }
}
