using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBlink : MonoBehaviour
{
    [SerializeField]
    private HideSystem _linkedSystem;

    [SerializeField]
    private Animator _animSrc;

    private void OnEnable()
    {
        this._animSrc.SetBool("Blinking", this._linkedSystem.GetHuntingStatus());
    }
}
