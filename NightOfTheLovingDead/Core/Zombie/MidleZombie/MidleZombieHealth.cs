using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidleZombieHealth : MonoBehaviour, IDamagble
{
    [SerializeField]
    private float _healthCount;

    public event System.Action<string> OnZombieDie;

    public void AddHealth(float healthCount)
    {
        this._healthCount = healthCount;
    }

    public void TakeDamage(float damage, string plrName)
    {
        this._healthCount -= damage;

        if (this._healthCount <= 0)
            this.OnZombieDie?.Invoke(plrName);

    }
}
