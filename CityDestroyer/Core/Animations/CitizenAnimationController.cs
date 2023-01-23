using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenAnimationController : MonoBehaviour
{
    [SerializeField]
    private List<Animator> _citizenControlles;

    [SerializeField]
    private bool _menuAnimation;

    // Start is called before the first frame update
    void Start()
    {
        if (this._menuAnimation)
        {
            ChangeExitPermission(null, 2);
            PlayDanceAnimations(null,null);
        }
        else
        {
            SetRandomBlend();

            EventBus.SubscribeToEvent(EventType.STAR_TAKED, PlayDanceAnimations);
            EventBus.SubscribeToEvent(EventType.STAR_TAKED, ChangeExitPermission);
        }
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.STAR_TAKED, PlayDanceAnimations);
        EventBus.UnsubscribeFromEvent(EventType.STAR_TAKED, ChangeExitPermission);
    }

    private void SetRandomBlend()
    {
        for (int i = 0; i < this._citizenControlles.Count; i++)
        {
            this._citizenControlles[i].SetFloat("Blend", Random.Range(0f,1f));
        }
    }

    private void PlayDanceAnimations(object sender, object param)
    {
        for (int i = 0; i < this._citizenControlles.Count; i++)
        {
            int animTrigger = Random.Range((int)AnimationTriggerNames.Dance1, (int)AnimationTriggerNames.Dance8 + 1);
            this._citizenControlles[i].SetTrigger(((AnimationTriggerNames)animTrigger).ToString());
        }
    }

    private void ChangeExitPermission(object sender, object param)
    {
        if ((int)param == 2)
        {
            for (int i = 0; i < this._citizenControlles.Count; i++)
            {
                this._citizenControlles[i].SetBool("CanReturnToBase", false);
            }
        }
    }

    enum AnimationTriggerNames
    {
        Dance1,
        Dance2,
        Dance3, 
        Dance4,
        Dance5,
        Dance6,
        Dance7,
        Dance8
    }
}
