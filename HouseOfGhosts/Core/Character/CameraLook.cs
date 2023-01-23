using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
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

    private void Start()
    {
        EventBus.SubscribeToEvent(EventType.DEMON_BURNED, DisableComponent);
        EventBus.SubscribeToEvent(EventType.SENSVALUE_CHANGED, OnSensetivityChanged);

        if (SensetivitySettings.Instance != null)
        {
            this._sensetivityX = this._sensetivutyY = SensetivitySettings.Instance.SensetivityValue;
        }
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.DEMON_BURNED, DisableComponent);
        EventBus.UnsubscribeFromEvent(EventType.SENSVALUE_CHANGED, OnSensetivityChanged);
    }

    private void Update()
    {
        this.transform.Rotate(Vector3.up, this._mouseX * Time.deltaTime);

        this._xRotation -= this._mouseY;
        this._xRotation = Mathf.Clamp(this._xRotation, -this._xAngle, this._xAngle);

        Vector3 targetRotation = this.transform.eulerAngles;
        targetRotation.x = this._xRotation;

        this._camTransform.eulerAngles = targetRotation;
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        this._mouseX = mouseInput.x * this._sensetivityX;
        this._mouseY = mouseInput.y * this._sensetivutyY * Time.deltaTime;
    }

    private void DisableComponent(object sender, object param)
    {
        this.enabled = false;
    }

    private void OnSensetivityChanged(object sender, object param)
    {
        this._sensetivityX = this._sensetivutyY = (float)param;
    }
}
