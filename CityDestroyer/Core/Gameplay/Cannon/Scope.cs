using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    private LineRenderer _scopeLine;

    [SerializeField]
    private Transform _muzzle;

    [SerializeField]
    private float _lineLenght;

    // Start is called before the first frame update
    void Start()
    {
        this._scopeLine = GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        RaycastHit hit = CreateRaycast(this._lineLenght);
        Vector3 enpPosition = this._muzzle.position + (this._muzzle.forward * this._lineLenght);

        if (hit.collider == null)
        {
            enpPosition = hit.point;
        }

        this._scopeLine.SetPosition(0, this.transform.position);
        this._scopeLine.SetPosition(1, enpPosition);
    }

    private RaycastHit CreateRaycast(float lenght)
    {
        RaycastHit hit;
        Ray ray = new Ray(this._muzzle.position, this._muzzle.forward);

        Physics.Raycast(ray, out hit, lenght);

        return hit;
    }
}
