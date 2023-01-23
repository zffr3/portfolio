using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        this._animator = this.GetComponent<Animator>();
    }

    public void SetAnimationState(NpcStates state)
    {
        DisableStates();
        this._animator.SetBool(state.ToString(),true);
    }

    private void DisableStates()
    {
        for (int i = 0; i < 5; i++)
            this._animator.SetBool(((NpcStates)i).ToString(),false);
    }
}
