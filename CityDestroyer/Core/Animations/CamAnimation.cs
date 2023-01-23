using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _camAnimSrc;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.SubscribeToEvent(EventType.STAR_TAKED, PlayCamAnim);
    }
    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.STAR_TAKED, PlayCamAnim);
    }

    private void PlayCamAnim(object sender, object param)
    {
        if ((int)param == 2)
        {
            this._camAnimSrc.enabled = true;
            this._camAnimSrc.SetTrigger("Play");
        }
    }

    public void HandleEndOfAnimation()
    {
        EventBus.Dispath(EventType.CAM_ANIMATION_ENDED, this,this);
    }
}
