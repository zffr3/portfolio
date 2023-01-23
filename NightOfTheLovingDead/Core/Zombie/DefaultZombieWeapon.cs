using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefaultZombieWeapon : MonoBehaviour, IZombieWeapon
{
    [SerializeField]
    private float _attackDistance;
    [SerializeField]
    private float _damage;

    [SerializeField]
    private WalkinZombieAnimation _animation;

    [SerializeField]
    private NavMeshAgent _agent;

    private bool _canAttack;

    public void Attack(Human target)
    {
        if (target != null)
        {
            if (Vector3.Distance(this.transform.position, target.transform.position) < this._attackDistance)
            {
                Human hSrc = target;

                if (hSrc != null && hSrc.CurrentType != HumanType.Zombie && this._canAttack)
                {
                    this._animation.Attack();
                    hSrc.GetComponent<IDamagble>().TakeDamage(this._damage, "Zombie");

                    this._canAttack = false;
                    if (this._agent != null)
                    {
                        this._agent.enabled = false;
                    }

                    StartCoroutine(RessetAttackKD());
                }
            }
        }
    }

    public void ConfigureWeapon(float attackDistance, float damage)
    {
        this._damage = damage;
        this._attackDistance = attackDistance;
        this._canAttack = true;

        this._animation = this.GetComponentInChildren<WalkinZombieAnimation>();
    }

    IEnumerator RessetAttackKD()
    {
        yield return new WaitForSeconds(0.5f);
        this._canAttack = true;
        if (this._agent !=null)
        {
            this._agent.enabled = true;
        }

    }
}
