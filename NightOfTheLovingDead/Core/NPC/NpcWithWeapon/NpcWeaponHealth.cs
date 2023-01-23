using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWeaponHealth : MonoBehaviour, IDamagble
{
    [SerializeField]
    private float _health;

    public void AddHealth(float healthCount)
    {
        this._health += healthCount;
    }

    public void TakeDamage(float damage, string plrName)
    {
        this._health -= damage;
        if (this._health <= 0)
        {
            this.GetComponent<NpcWeaponAi>().Die();
        }
    }

}
