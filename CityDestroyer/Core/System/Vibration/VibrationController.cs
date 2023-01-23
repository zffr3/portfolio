using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventBus.SubscribeToEvent(EventType.SHOT_BTN_PRESSED, Vibrate);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.SHOT_BTN_PRESSED, Vibrate);
    }

    private void Vibrate(object sender, object param)
    {
        VibrationService.Vibrate();
    }
}
