using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventDisable : MonoBehaviour
{
    [SerializeField]
    private AnimationEvent _linkedEvent;

    public void HandleEndOfAnimation()
    {
        this._linkedEvent.StopBlinking();
        if (this._linkedEvent != null)
        {
            this._linkedEvent.StopBlinking();
        }

        this.gameObject.SetActive(false);
    }
}
