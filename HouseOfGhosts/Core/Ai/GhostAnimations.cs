using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAnimations : MonoBehaviour
{
    [SerializeField]
    private Animator _animatorSrc;

    [SerializeField]
    private NavMeshAgent _agent;

    private void Start()
    {
        this._agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (this._animatorSrc != null && this._agent != null)
        {
            this._animatorSrc.SetFloat("Speed", this._agent.velocity.magnitude);
        }
    }

    public void SetNewAnimator(Animator newAnimator)
    {
        this._animatorSrc = newAnimator;
    }

    public void ChangeDemonVariable(bool isDemon)
    {
        this._animatorSrc.SetBool("Demon",isDemon);
    }
}
