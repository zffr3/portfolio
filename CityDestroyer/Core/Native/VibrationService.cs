using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VibrationService
{

//#if UNITY_ANDROID && !UNITY_EDITOR
//    private static AndroidJavaClass _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//    private static AndroidJavaObject _currentActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
//    private static AndroidJavaObject _vibrationService = _currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
//#else
//    private static AndroidJavaClass _unityPlayer;
//    private static AndroidJavaObject _currentActivity;
//    private static AndroidJavaObject _vibrationService;
//#endif

    public static void Vibrate(long milliseconds = 250)
    {
    //    if (GetIsOsAndriod())
    //    {
    //        _vibrationService.Call("vibrate", milliseconds);
    //    }
    //    else
    //    {
    //        Handheld.Vibrate();
    //    }
    }

    private static void Cancel()
    {
        //if (GetIsOsAndriod())
        //{
        //    _vibrationService.Call("cancel");
        //}
    }

    private static bool GetIsOsAndriod()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }
}
