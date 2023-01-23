using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRaycaster : MonoBehaviour
{
    [SerializeField]
    private Player _playerSource;

    [SerializeField]
    private float _rayLenght;
    [SerializeField]
    private float _radius;

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen

        // actual Ray
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        // debug Ray
        Debug.DrawRay(ray.origin, ray.direction * this._rayLenght, Color.red);

        RaycastHit hit;
        //if (Physics.SphereCast(ray, this._radius, out hit, this._rayLenght))
        if(Physics.Raycast(ray, out hit, this._rayLenght))
        {
            DetermineTarget(hit.collider);
        }
    }

    private void DetermineTarget(Collider target)
    {
        if (target.tag == "Interactable")
        {
            GameUi.instance.ChangeTakeButtonState(true);
            this._playerSource.SetRayTarget(target.gameObject);
        }
        else
        {
            GameUi.instance.ChangeTakeButtonState(false);
            this._playerSource.SetRayTarget(null);
        }
    }
}
