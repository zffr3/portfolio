using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimationEventHandler : MonoBehaviour
{
    public void HandleEndOfAnimation()
    {
        this.gameObject.SetActive(false);
        EventBus.Dispath(EventType.CAR_ANIM_ENDED, this, this.gameObject);
    }
}
