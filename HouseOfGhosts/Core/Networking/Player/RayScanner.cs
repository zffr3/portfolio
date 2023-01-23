using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScanner : MonoBehaviour
{
    [SerializeField]
    private float _rayLenght;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen

        // actual Ray
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        RaycastHit hit;
        //if (Physics.SphereCast(ray, this._radius, out hit, this._rayLenght))
        if (Physics.Raycast(ray, out hit, this._rayLenght))
        {
            if (hit.collider.tag == "Key")
            {

            }
        }
    }
}
