using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBarrel : MonoBehaviour, IDamagble
{
    [SerializeField]
    private GameObject _explosionObject;
    [SerializeField]
    private GameObject _killAura;

    [SerializeField]
    private float _health;

    private bool _isExploded;

    public void AddHealth(float healthCount)
    {
        this._health += healthCount;
    }

    public void TakeDamage(float damage, string plrName)
    {
        this._health -= damage;
        if (this._health <= 0)
            Explode();

    }

    private void Explode()
    {
        if (this._explosionObject != null)
        {
            this._isExploded = true;
            this._killAura.SetActive(true);
            Instantiate(this._explosionObject, this.transform.position + Vector3.up, this.transform.rotation);

            StartCoroutine(DestroyWithDelay());
        }
    }

    IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
