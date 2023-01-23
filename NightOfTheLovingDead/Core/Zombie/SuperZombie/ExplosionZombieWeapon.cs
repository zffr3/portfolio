using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionZombieWeapon : MonoBehaviour, IZombieWeapon
{
    [SerializeField]
    private GameObject _explosionObject;
    [SerializeField]
    private GameObject _killAura;

    private bool _isExploded;

    public void Attack(Human target)
    {
        if (!this._isExploded)
        {
            this._isExploded = true;
            this._killAura.SetActive(true);
            Instantiate(this._explosionObject, this.transform.position + Vector3.up, this.transform.rotation);
        }
    }

    public void ConfigureWeapon(float attackDistance, float damage)
    {

    }
}
