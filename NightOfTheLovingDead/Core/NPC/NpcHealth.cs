using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHealth : MonoBehaviour, IDamagble
{
    [SerializeField]
    private float _healthCount;


    // Start is called before the first frame update
    void Start()
    {
        this._healthCount = 100;
    }

    public void AddHealth(float healthCount)
    {
        this._healthCount += healthCount;
    }

    public void TakeDamage(float damage, string plrName)
    {
        if (plrName == "Zombie")
            damage /= 5;

        this._healthCount -= damage;

        if (this._healthCount <= 0)
        {
            this.GetComponent<INpc>().Die();
            DestroyNpc();
        }
    }

    private void DestroyNpc()
    {
        GameObject.Destroy(this.gameObject);
    }
}
