using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RZHealth : MonoBehaviour, IDamagble
{
    [SerializeField]
    private float _healthCount;

    public void AddHealth(float healthCount)
    {
        this._healthCount = healthCount;
    }

    public void TakeDamage(float damage, string plrName)
    {
        this._healthCount -= damage;
        if (this._healthCount <= 0)
            this.GetComponent<ReactiveZombie>().CallDestroyZombie(true);
    }
}
