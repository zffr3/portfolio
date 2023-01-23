using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidleZombieWeapon : MonoBehaviour, IZombieWeapon
{
    [SerializeField]
    private WalkinZombieAnimation _animation;

    private float _attackDistance;
    private float _damage;

    [SerializeField]
    private Human _target;

    public void Attack(Human target)
    {
        if (this._target == null || this._target != target)
            this._target = target;

        if (this._animation == null)
            this._animation = this.GetComponent<WalkinZombieAnimation>();

        Ray ray = new Ray(this.transform.position, this._target.transform.position - this.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, this._attackDistance))
        {
            if (hit.collider.GetComponent<Human>() != null)
            {
                hit.collider.GetComponent<IDamagble>().TakeDamage(this._damage, "Zombie");
            }
            this._animation.Attack();
        }
    }

    public void ConfigureWeapon(float attackDistance, float damage)
    {
        this._attackDistance = attackDistance;
        this._damage = damage;
    }
}
