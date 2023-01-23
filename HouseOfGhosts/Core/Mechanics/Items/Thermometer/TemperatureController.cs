using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureController : MonoBehaviour
{
    public static TemperatureController instance;

    [SerializeField]
    private float _minTemp;
    [SerializeField]
    private float _maxTemp;

    public float MinTemp
    {
        get { return this._minTemp; }
    }
    public float MaxTemp
    {
        get { return this._maxTemp; }
    }

    [SerializeField]
    private float _cursedOffset;
    private bool _cursed;
    [SerializeField]
    private float _ghostOffset;
    private bool _isGhost;

    private void Awake()
    {
        instance = this;
    }

    public void GhostEnter()
    {
        this._isGhost = true;

        this._minTemp -= this._ghostOffset;
        this._maxTemp -= this._ghostOffset;
    }

    public void GhostExit()
    {
        this._isGhost = false;

        this._minTemp += this._ghostOffset;
        this._maxTemp += this._ghostOffset;
    }

    public void ChangeTemperatureZone(bool zoneWithGhost)
    {
        if (zoneWithGhost)
        {
            if (this._isGhost)
            {
                return;
            }
            this._isGhost = true;

            this._minTemp -= this._ghostOffset;
            this._maxTemp -= this._ghostOffset;
        }
        else
        {
            if (this._isGhost)
            {
                this._isGhost = false;

                this._minTemp += this._ghostOffset;
                this._maxTemp += this._ghostOffset;
            }
        }
    }
}
