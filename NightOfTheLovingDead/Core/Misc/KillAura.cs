using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAura : MonoBehaviour
{
    [SerializeField]
    private float _damage;

    [SerializeField]
    private bool _killSource;

    private void Start()
    {
        StartCoroutine(Explode());
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagble healthSrc = other.GetComponent<IDamagble>();
        if (healthSrc != null)
            healthSrc.TakeDamage(this._damage, "0xExplosion");
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.05f);
        
        if (this._killSource)
            this.GetComponentInParent<IDamagble>().TakeDamage(1000, "0xExplosion");
    }
}
