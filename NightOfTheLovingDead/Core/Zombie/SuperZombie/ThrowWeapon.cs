using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour, IZombieWeapon
{
    [SerializeField]
    private GameObject _throwObject;
    [SerializeField]
    private Human _target;

    [SerializeField]
    private Transform _startPosition;

    [SerializeField]
    private float _speed = 500;

    [SerializeField]
    private float _throwCoolDown;

    [SerializeField]
    private bool _canThrow;

    public void Attack(Human target)
    {
        if (this._canThrow)
        {
            if (target != this._target)
                this._target = target;

            this._canThrow = false;

            ThrowTarget(target.transform.position);
            StartCoroutine(ResetThrow());
        }

    }

    public void ConfigureWeapon(float attackDistance, float damage)
    {
    }

    [PunRPC]
    private void ThrowTarget(Vector3 target)
    {
        GameObject projectile = Instantiate(this._throwObject, this._startPosition.position, Quaternion.identity) as GameObject;

        projectile.transform.LookAt(target + Vector3.up);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * this._speed);
    }

    IEnumerator ResetThrow()
    {
        yield return new WaitForSeconds(this._throwCoolDown);
        this._canThrow = true;
    }
}
