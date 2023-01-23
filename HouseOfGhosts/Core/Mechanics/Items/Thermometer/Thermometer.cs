using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Thermometer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _display;

    [SerializeField]
    private float _delay;

    [SerializeField]
    private float _minTemperature;
    [SerializeField]
    private float _maxTemperature;

    private Character _characterSource;

    [SerializeField]
    private bool _useAsMarker;
    private bool _activated;

    private void OnEnable()
    {
        if (!this._useAsMarker)
        {
            StartCoroutine(ChangeTemperature());
        }
        else
        {
            this._characterSource = GetComponentInParent<Character>();
            StartCoroutine(ChangeTemperatueMarker());
            this._activated = false;
        }
    }


    IEnumerator ChangeTemperature()
    {
        this._minTemperature = TemperatureController.instance.MinTemp;
        this._maxTemperature = TemperatureController.instance.MaxTemp;

        this._display.text = (System.Math.Round(Random.Range(this._minTemperature, this._maxTemperature),2)).ToString();

        yield return new WaitForSeconds(this._delay);
        StartCoroutine(ChangeTemperature());

    }

    IEnumerator ChangeTemperatueMarker()
    {
        this._activated = this._characterSource.GetCurrentMarker() == CursedRoomMarkers.Thermometr;

        if (this._activated)
        {
            this._minTemperature = -15;
            this._maxTemperature = -5;
        }
        else
        {
            this._minTemperature = 15;
            this._maxTemperature = 25;
        }

        this._display.text = (System.Math.Round(Random.Range(this._minTemperature, this._maxTemperature), 2)).ToString();

        yield return new WaitForSeconds(this._delay);
        StartCoroutine(ChangeTemperature());
    }
}
