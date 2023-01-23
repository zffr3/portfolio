using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GhostMove : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _target;

    private bool _canMoving;

    // Start is called before the first frame update
    void Start()
    {
        this._agent = GetComponent<NavMeshAgent>();
        this._canMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this._target != null && this._agent != null)
        {
            if (this._canMoving)
            {
                this._agent.SetDestination(this._target.position);
            }
        }
    }

    public void SetTarget(Transform target)
    {
        this._target = target;
    }
    public void ChangeMovmentFlag(bool canMove)
    {
        this._canMoving = canMove;
    }
}
