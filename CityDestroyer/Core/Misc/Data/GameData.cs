using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private const float DEFAULT_SHOT_POWER = 7500f;
    private const float DEFAULT_BULLET_WEIGHT = 15f;

    [SerializeField]
    private float _shotPower;
    [SerializeField]
    private float _bulletWeight;

    [SerializeField]
    private int _starCount;

    // Start is called before the first frame update
    void Start()
    {
        ReadData();

        EventBus.SubscribeToEvent(EventType.STARS_GIVEN_COMPLETE, AddStars);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.STARS_GIVEN_COMPLETE, AddStars);
    }

    public void AddStars(object sender, object param)
    {
        this._starCount += (int)param+1;
        UpdateIntData("Stars", this._starCount);
        EventBus.Dispath(EventType.DATA_SHOP_LOADED, this._starCount, 0);
    }

    public bool TakeStar(int starCount)
    {
        if (this._starCount - starCount >= 0)
        {
            this._starCount -= starCount;
            UpdateIntData("Stars", this._starCount);
            EventBus.Dispath(EventType.DATA_SHOP_LOADED, this._starCount, 0);
            return true;
        }
        return false;
    }

    public void AddShotPower(float powerRate)
    {
        this._shotPower += powerRate;
        UpdateFloatData("ShotPower", this._shotPower);
    }

    public void AddBulletWeight(float weight)
    {
        this._bulletWeight += weight;
        UpdateFloatData("BulletWeight", this._bulletWeight);
    }

    private void UpdateIntData(string dataKey, int data)
    {
        PlayerPrefs.SetInt(dataKey, data);
    }

    private void UpdateFloatData(string dataKey, float data)
    {
        PlayerPrefs.SetFloat(dataKey, data);
    }

    private void ReadData()
    {
        this._shotPower = PlayerPrefs.GetFloat("ShotPower");
        this._bulletWeight = PlayerPrefs.GetFloat("BulletWeight");
        this._starCount = PlayerPrefs.GetInt("Stars");

        this._shotPower = this._shotPower == 0 ? DEFAULT_SHOT_POWER : this._shotPower;
        this._bulletWeight = this._bulletWeight == 0 ? DEFAULT_BULLET_WEIGHT : this._bulletWeight;

        EventBus.Dispath(EventType.DATA_CB_LOADED, this._shotPower, this._bulletWeight);
        EventBus.Dispath(EventType.DATA_SHOP_LOADED, this._starCount,0);
    }
}
