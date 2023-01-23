using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestruction : MonoBehaviour
{
    [SerializeField]
    private bool _sentDamageAfterDestroy;

    [SerializeField]
    private float _damage;
    [SerializeField]
    private string _damageMessage;

    private void OnCollisionEnter(Collision collision)
    {
        if (this._sentDamageAfterDestroy)
        {
            IDamagble dmgSrc = collision.gameObject.GetComponent<IDamagble>();
            if (dmgSrc == null)
                dmgSrc = collision.gameObject.GetComponentInParent<IDamagble>();

            dmgSrc?.TakeDamage(this._damage, this._damageMessage);
        }

        ColisionDestroyBullet();
    }

    private void ColisionDestroyBullet()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void ConfigureBullet(float damage, string message)
    {
        this._damage = damage;
        this._damageMessage = message;
    }
}
