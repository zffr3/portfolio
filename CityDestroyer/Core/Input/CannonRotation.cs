using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{
    private CannonInputAction _actionSrc;
    private CannonInputAction.CannonActions _cannonActions;

    [SerializeField]
    private float _sensetivityX;
    [SerializeField]
    private float _sensetivutyY;

    [SerializeField]
    private float _clampedYMin;
    [SerializeField]
    private float _clampedYMax;

    [SerializeField]
    private float _clampedXMin;
    [SerializeField]
    private float _clampedXMax;

    [SerializeField]
    private Transform _cannonTransform;

    private float _mouseX;
    private float _mouseY;

    private void Start()
    {
        this._actionSrc = new CannonInputAction();
        this._cannonActions = this._actionSrc.Cannon;

        this._actionSrc.Enable();
#if UNITY_EDITOR
        this._cannonActions.xAxis.performed += context => this._mouseX = context.ReadValue<float>() * this._sensetivityX * Time.deltaTime;
        this._cannonActions.yAxis.performed += context => this._mouseY = context.ReadValue<float>() * this._sensetivutyY * Time.deltaTime;
#else
        this._cannonActions.xAxis.performed += context => this._mouseX += context.ReadValue<float>() * this._sensetivityX * Time.deltaTime;
        this._cannonActions.yAxis.performed += context => this._mouseY -= context.ReadValue<float>() * this._sensetivutyY *  Time.deltaTime;
#endif
    }

    private void OnDestroy()
    {
        this._actionSrc.Disable();
    }

    private void Update()
    {
        if (this._cannonActions.AllowRotation.ReadValue<float>() != 0)
        {
#if UNITY_EDITOR
            Vector3 yRotation = new Vector3(0, this._mouseX, 0);
            this.transform.Rotate(yRotation);

            this._cannonTransform.Rotate(new Vector3(-(this._mouseY), 0f, 0f));
#else
            this._mouseX = Mathf.Clamp(this._mouseX, this._clampedYMin, this._clampedYMax);
            this.transform.eulerAngles = new Vector3(0, this._mouseX, 0);

            this._mouseY = Mathf.Clamp(this._mouseY, this._clampedXMin, this._clampedXMax);
            this._cannonTransform.localEulerAngles = new Vector3(this._mouseY, 0, 0);
#endif
        }
    }
}
