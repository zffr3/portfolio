using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletObject;
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private Transform _startPosition;

    [SerializeField]
    private float _speed = 500;

    [SerializeField]
    private bool _canShot;

    public void SetTarget(GameObject target)
    {
        this._target = target;
    }

    public void DropTarget()
    {
        this._target = null;
    }

    public void Shot()
    {
        if (this._canShot && this._target != null)
        {
            this._canShot = false;

            ThrowTarget(this._target.transform.position);
            StartCoroutine(ResetThrow());
        }
    }

    private void ThrowTarget(Vector3 targetPos)
    {
        GameObject projectile = Instantiate(this._bulletObject, this._startPosition.position, Quaternion.identity) as GameObject;

        projectile.GetComponent<BulletDestruction>().ConfigureBullet(15f, "NPC");
        projectile.transform.LookAt(targetPos + Vector3.up);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * this._speed);
    }

    IEnumerator ResetThrow()
    {
        yield return new WaitForSeconds(0.35f);
        this._canShot = true;
    }
}
