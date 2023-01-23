using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkDestroyer : MonoBehaviour
{

    [SerializeField]
    private float _destroingDelay;

    // Start is called before the first frame update
    void Start()
    {

        this.StartCoroutine(DestroyObjects());
    }

    IEnumerator DestroyObjects()
    {
        yield return new WaitForSeconds(this._destroingDelay);
        GameObject.Destroy(this.gameObject);
    }
}
