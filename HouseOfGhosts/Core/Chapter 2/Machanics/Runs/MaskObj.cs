using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskObj : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _maskObjects;

    [SerializeField]
    private float _rayLenght;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this._maskObjects.Length; i++)
        {
            this._maskObjects[i].GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        //if (Physics.SphereCast(ray, this._radius, out hit, this._rayLenght))
        if (Physics.Raycast(ray, out hit, this._rayLenght))
        {
            this.transform.position = hit.point;
        }
    }
}
