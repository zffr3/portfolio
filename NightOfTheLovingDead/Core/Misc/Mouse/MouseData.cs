using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseData 
{
    private static float _sensetivytyData;
    public static float SensetivytyData 
    {
        get 
        {
            return _sensetivytyData;
        } 
        set
        {
            _sensetivytyData = value;
            EventBus.Dispath(EventType.SENS_CHANGED, value, value);
        }
    }
}
