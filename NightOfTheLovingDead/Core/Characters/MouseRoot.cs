using UnityEngine;

public class MouseRoot : MonoBehaviour
{
    [SerializeField]
    private RotationAxes _currentAxes;

    [SerializeField]
    private float _sensitivityAxes;

    [SerializeField]
    private float _minVert;
    [SerializeField]
    private float _maxVert;

    private float _rotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody body = this.GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }

        EventBus.SubscribeToEvent(EventType.SENS_CHANGED, OnSensitivityChanged);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.SENS_CHANGED, OnSensitivityChanged);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (this._currentAxes == RotationAxes.AxisX)
        {
            this.transform.Rotate(0, PlayerKeys.Instance.MouseX * this._sensitivityAxes * Time.deltaTime, 0);
        }
        else if (this._currentAxes == RotationAxes.AxisY)
        {
            this._rotationX -= PlayerKeys.Instance.MouseY * this._sensitivityAxes * Time.deltaTime;
            this._rotationX = Mathf.Clamp(this._rotationX, this._minVert, this._maxVert);

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(this._rotationX, rotationY, 0);
        }
        else
        {
            this._rotationX -= PlayerKeys.Instance.MouseY * this._sensitivityAxes * Time.deltaTime;
            this._rotationX = Mathf.Clamp(this._rotationX, this._minVert, this._maxVert);

            float delta = PlayerKeys.Instance.MouseX * this._sensitivityAxes * Time.deltaTime;
            float rotationY = this.transform.localEulerAngles.y + delta;

            this.transform.localEulerAngles = new Vector3(this._rotationX, rotationY, 0);
        }

    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) 
        {
            lfAngle += 360f;
        }
        if (lfAngle > 360f)
        {
            lfAngle -= 360f;
        }

        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void OnSensitivityChanged(object sender, object param)
    {
        this._sensitivityAxes = (float)param;
    }

    public enum RotationAxes
    {
        AxisX,
        AxisY,
        AxisXandY
    }
}
