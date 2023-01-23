using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDestroyer : MonoBehaviour
{
    [SerializeField]
    private float _delayTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyWithDelay());
    }

    IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(this._delayTime);

        Destroy(this.gameObject);
    }
}
