using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _animSrc;

    // Start is called before the first frame update
    void Start()
    {
        if (this._animSrc == null)
        {
            this._animSrc = GetComponentInChildren<Animator>();
        }

        this._animSrc.SetBool("Demon", true);
        this._animSrc.SetTrigger("Attack1");

       
    }
}
