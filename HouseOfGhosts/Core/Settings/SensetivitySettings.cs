using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensetivitySettings : MonoBehaviour
{
    public static SensetivitySettings Instance { get; private set; }

    [SerializeField]
    private Slider _sensetivitySlider;

    private float _sensetivityValue;
    public float SensetivityValue 
    {
        get
        {
            return _sensetivityValue;
        }
        private set 
        {
            this._sensetivityValue = value;
            PlayerPrefs.SetFloat("Sens", value);
            EventBus.Dispath(EventType.SENSVALUE_CHANGED, this, value);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        this._sensetivityValue = PlayerPrefs.GetFloat("Sens");
        if (this._sensetivityValue == 0)
        {
            SetSensetivity(1f);
        }

        this._sensetivitySlider.value = this._sensetivityValue;
        EventBus.Dispath(EventType.SENSVALUE_CHANGED, this, this);
    }

    public void SetSensetivity()
    {
        SetSensetivity(this._sensetivitySlider.value);
    }

    private void SetSensetivity(float value)
    {
        SensetivityValue = value;
    }
}
