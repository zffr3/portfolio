using System;
using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class WZombieHealts : MonoBehaviour,IDamagble
{
    [SerializeField]
    private float _healthCount;

    [SerializeField]
    private TMP_Text _healthText;

    public void AddHealth(float healthCount)
    {
        this._healthCount = healthCount;
    }

    public void TakeDamage(float damage, string plrName)
    {
        this._healthCount -= damage;
        if (!this._healthText.gameObject.activeSelf && this._healthCount > 0)
        {
            StartCoroutine(DisplayHealth(this._healthCount));
        }
        this._healthText.text = this._healthCount.ToString();

        if (this._healthCount <= 0)
        {
            this.GetComponent<WalkingZombieAI>().CallDestroyZombie(plrName);
        }
    }

    private IEnumerator DisplayHealth(float actualHealthCount)
    {
        if (!this._healthText.gameObject.activeSelf)
        {
            this._healthText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        this._healthText.gameObject.SetActive(false);
    }
}
