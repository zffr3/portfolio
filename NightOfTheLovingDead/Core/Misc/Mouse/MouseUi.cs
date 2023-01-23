using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseUi : MonoBehaviour
{
    [SerializeField]
    private Slider _sensSlider;

    private void Start()
    {
        if (this._sensSlider == null)
        {
            this._sensSlider = this.GetComponent<Slider>();
        }

        this._sensSlider.value = MouseData.SensetivytyData;
    }

    public void ChangeSensValue()
    {
        MouseData.SensetivytyData = this._sensSlider.value;
        PlayerPrefs.SetFloat("Sens", this._sensSlider.value);
    }
}
