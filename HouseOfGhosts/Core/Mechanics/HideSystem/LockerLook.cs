using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerLook : MonoBehaviour
{
    [SerializeField]
    private float _sensetivityX;
    [SerializeField]
    private float _sensetivutyY;

    [SerializeField]
    private Transform _camTransform;
    [SerializeField]
    float _xAngle;
    float _xRotation;

    private float _mouseX;
    private float _mouseY;

    private void Update()
    {
        this._camTransform.Rotate(Vector3.up, Mathf.Clamp(this._mouseX * Time.deltaTime, -this._xAngle, this._xAngle));

        this._xRotation -= this._mouseY;
        this._xRotation = Mathf.Clamp(this._xRotation, -this._xAngle, this._xAngle);

        Vector3 targetRotation = this.transform.eulerAngles;
        targetRotation.x = this._xRotation;

        this._camTransform.eulerAngles = targetRotation;
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        this._mouseX = mouseInput.x * this._sensetivityX * Time.deltaTime;
        this._mouseY = mouseInput.y * this._sensetivutyY * Time.deltaTime;
    }
}
