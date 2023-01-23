using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    [SerializeField]
    private Transform _recoilPosition;
    [SerializeField]
    private Transform _rotationPoint;

    [SerializeField]
    private float _positionalRecoilSpeed = 8f;
    [SerializeField]
    private float _rotationalRecoilSpeed = 8f;

    [SerializeField]
    private float _positionalReturnSpeed = 18f;
    [SerializeField]
    private float _rotationalReturnSpeed = 38f;

    [SerializeField]
    private Vector3 _recoilRotation = new Vector3(10,5,7);
    [SerializeField]
    private Vector3 _recoilKickBack = new Vector3(0.2f,-0.305f,-0.238f);

    private Vector3 _rotationalRecoil;
    private Vector3 _positionalRecoil;
    private Vector3 _rotation;

    private void Start()
    {
        this._recoilPosition = this.transform;
        this._rotationPoint = this.transform;
    }

    private void FixedUpdate()
    {
        this._rotationalRecoil = Vector3.Lerp(this._rotationalRecoil,Vector3.zero, this._rotationalReturnSpeed * Time.deltaTime);
        this._positionalRecoil = Vector3.Lerp(this._positionalRecoil,Vector3.zero,this._positionalReturnSpeed * Time.deltaTime);

        this._recoilPosition.localPosition = Vector3.Slerp(this._recoilPosition.localPosition, this._positionalRecoil, this._positionalRecoilSpeed * Time.fixedDeltaTime);
        this._rotation = Vector3.Slerp(this._rotation,this._rotationalRecoil ,this._rotationalRecoilSpeed * Time.fixedDeltaTime);
        this._rotationPoint.localRotation = Quaternion.Euler(this._rotation);

    }

    public void TakeRecoil()
    {
        this._rotationalRecoil += new Vector3(-this._recoilRotation.x, Random.Range(-this._recoilRotation.y,this._recoilRotation.y), Random.Range(-this._recoilRotation.z, this._recoilRotation.z));
        this._positionalRecoil += new Vector3(Random.Range(-this._recoilKickBack.x, this._recoilKickBack.x), Random.Range(-this._recoilKickBack.y, this._recoilKickBack.y), this._recoilKickBack.z);
    }
}
