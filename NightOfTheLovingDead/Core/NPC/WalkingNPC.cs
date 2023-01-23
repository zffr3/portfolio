using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingNPC : MonoBehaviour, INpc
{
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private NpcAnimation _animSrc;

    [SerializeField]
    private Settlement _settlementSrc;

    [SerializeField]
    private GameObject[] _skins;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _changeStateTimeOut;

    private NpcStates _currentState;

    public event System.Action<GameObject> OnNpcDie;

    // Update is called once per frame
    void Update()
    {
        if (this._target != null)
        {
            this._agent.SetDestination(this._target.position);

            if (Vector3.Distance(this.transform.position,this._target.transform.position) <= 1f)
                this._animSrc.SetAnimationState(NpcStates.Stay);
        }
    }

    public void Configure(Transform target, Settlement src, System.Action<GameObject> dieHandler)
    {
        this._target = target;
        this._settlementSrc = src;

        this._skins[Random.Range(0,this._skins.Length)].SetActive(true);

        this._agent = this.GetComponent<NavMeshAgent>();
        this._animSrc = this.GetComponent<NpcAnimation>();
        StartCoroutine(ChangeState());

        this.OnNpcDie += dieHandler;
    }

    private IEnumerator ChangeState()
    {
        this._currentState = (NpcStates)Random.Range(0, 4);

        if (this._currentState == NpcStates.Walk)
        {
            this._target = this._settlementSrc.GetRandomPoint();
        }
        else
        {
            this._target = null;
        }

        this._animSrc.SetAnimationState(this._currentState);

        yield return new WaitForSeconds(this._changeStateTimeOut);

        StartCoroutine(ChangeState());
    }

    public void Die()
    {
        this.OnNpcDie?.Invoke(this.gameObject);
        this.OnNpcDie = null;
    }
}
public enum NpcStates
{
    Stay,
    Walk,
    Dance,
    ShadowFight,
    Interact
}
