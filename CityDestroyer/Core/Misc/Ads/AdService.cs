using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdService : MonoBehaviour
{
    [SerializeField]
    private string _rewardStarStr;
    [SerializeField]
    private string _rewardBombStr;

    private void OnEnable()
    {
        YandexSDK.YaSDK.onRewardedAdReward += UserGotReward;
    }

    private void OnDisable()
    {
        YandexSDK.YaSDK.onRewardedAdReward -= UserGotReward;
    }

    public void ShowStartAd()
    {
        YandexSDK.YaSDK.instance.ShowRewarded(this._rewardStarStr);
    }

    public void ShowBombAd()
    {
        YandexSDK.YaSDK.instance.ShowRewarded(this._rewardBombStr);
    }

    public void ShowInterstitial()
    {
        YandexSDK.YaSDK.instance.ShowInterstitial();
    }

    private void UserGotReward(string rewardStr)
    {
        Debug.Log(rewardStr);
        if (rewardStr == this._rewardStarStr)
        {
            EventBus.Dispath(EventType.STARS_GIVEN_COMPLETE,this, 4);
        }

        if (rewardStr == this._rewardBombStr)
        {
            EventBus.Dispath(EventType.AD_COMPLETED, this, this);
        }
    }
}
