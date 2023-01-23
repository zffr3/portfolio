using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    private const float DEFAULT_WEIGHT_VALUE = 15f;
    private const float DEFAULT_POWER_VALUE = 7500f;

    public static UpgradeSystem Instance { get; private set; }

    [SerializeField]
    private GameData _gameData;

    [SerializeField]
    private float _weightValue;
    [SerializeField]
    private float _powerValue;

    private void Awake()
    {
        Instance = this;
        this._gameData = this.GetComponent<GameData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this._powerValue = PlayerPrefs.GetFloat("Power");
        if (this._powerValue == 0)
        {
            UpdateValue("Power", DEFAULT_POWER_VALUE);
        }

        this._weightValue = PlayerPrefs.GetFloat("Weight");
        if (this._weightValue == 0)
        {
            UpdateValue("Weight", DEFAULT_WEIGHT_VALUE);
        }
    }

    private void UpdateValue(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public float GetPowerWalue()
    {
        return this._powerValue;
    }

    public float GetWeight()
    {
        return this._weightValue;
    }

    public void UpgradePower()
    {
        if (this._gameData.TakeStar(6))
        {
            this._powerValue += this._powerValue * 0.25f;
            UpdateValue("Power", this._powerValue);
        }
    }

    public void UpgradeWeight()
    {
        if (this._gameData.TakeStar(6))
        {
            this._weightValue += this._weightValue * 0.25f;
            UpdateValue("Weight", this._weightValue);
        }
    }
}
