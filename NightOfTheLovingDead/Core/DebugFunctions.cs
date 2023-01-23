using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFunctions : MonoBehaviour
{
    [SerializeField]
    private Vector3 _spawnTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Configure(Vector3 spawnPoint)
    {
        this._spawnTransform = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            this.transform.position += new Vector3(0,85,0);
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            this.transform.position = this._spawnTransform;
        }
    }
}
