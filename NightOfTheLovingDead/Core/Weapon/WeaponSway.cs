using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField]
    private float _intensity;
    [SerializeField]
    private float _smooth;

    private Quaternion _originRotation;

    // Start is called before the first frame update
    void Start()
    {
        this._originRotation = this.transform.rotation;
    }

    private void CalculateSway()
    {
        float tX = PlayerKeys.Instance.MouseX,
            tY = PlayerKeys.Instance.MouseY;

        Quaternion xAdj = Quaternion.AngleAxis(-this._intensity * tX, Vector3.up),
            yAdj = Quaternion.AngleAxis(this._intensity * tY, Vector3.right),
            targetRotation = this._originRotation * xAdj * yAdj;

        this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, targetRotation, Time.deltaTime * this._smooth);
    }
}
