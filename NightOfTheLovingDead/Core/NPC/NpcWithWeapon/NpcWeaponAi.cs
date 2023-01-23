using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcWeaponAi : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _skins;
    [SerializeField]
    private NpcWeapon _linkedWeapon;
    [SerializeField]
    private NavMeshAgent _agentSrc;
    [SerializeField]
    private Animator _animSrc;

    [SerializeField]
    private NpcStates _currentState;

    [SerializeField]
    private NpcWeaponType _currentType;

    [SerializeField]
    private List<string> _friendList;

    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private Transform _rayStartPosition;
    [SerializeField]
    private int _angleBetwenTarget;

    [SerializeField]
    private bool _haveTarget;

    private bool _onTarget;

    [SerializeField]
    private PositionGraphNode _walkingPosition;

    public void ConfigureNpc(PositionGraphNode position)
    {
        this._onTarget = false;
        this._walkingPosition = position;
        this._skins[Random.Range(0,this._skins.Count)].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (this._haveTarget && this._target != null)
        {
            Quaternion look = Quaternion.LookRotation(this._target.transform.position - this.transform.position);
            if (look == Quaternion.identity)
                return;

            float angle = Quaternion.Angle(this.transform.rotation, look);
            if (angle < this._angleBetwenTarget)
            {
                this._onTarget = false;
                this._linkedWeapon.Shot();
                this.transform.LookAt(new Vector3(this._target.transform.position.x,this.transform.position.y, this._target.transform.position.z));
            }
            else
            {
                this._onTarget=true;
            }
        }

        if (this._walkingPosition == null)
            return;

        if (this._currentState == NpcStates.Walking && !this._onTarget)
            this._agentSrc.SetDestination(this._walkingPosition.transform.position);

        if (Vector3.Distance(this.transform.position, this._walkingPosition.transform.position) < 1f)
        {
            if (!this._onTarget)
            {
                this._onTarget = true;
                ChangeState();
            }
        }

        this._animSrc.SetBool(NpcStates.Walking.ToString(), this._agentSrc.velocity.magnitude > 0.01f);
    }

    public void HandleHumanEnter(GameObject human)
    {
        if (human == this.gameObject)
            return;

        if (this._haveTarget && this._target != null)
            if (!CheckDistanceBetweenTargets(human))
                return;

        this._target = human;
        this._haveTarget = true;
        this._linkedWeapon.SetTarget(this._target);

    }
    public void HandleHumanExit(GameObject human)
    {
        if (this._target == human)
        {
            this._haveTarget = false;
            this._target = null;
        }
    }

    public void AddPlayerToFriendList(string friend)
    {
        if (this._friendList == null)
            this._friendList = new List<string>();
        this._friendList.Add(friend);
    }

    public void RemovePlayerFromFriendList(string playerNick)
    {
        if (this._friendList == null)
            return;

        this._friendList.Remove(playerNick);
    }

    public void Die()
    {
        //Destroy or return to Object pool
        GameObject.Destroy(this.gameObject);
    }

    private bool CheckDistanceBetweenTargets(GameObject nTarget)
    {
        if (Vector3.Distance(this.transform.position, this._target.transform.position) > Vector3.Distance(this.transform.position, nTarget.transform.position))
            return true;
        return false;
    }

    private enum NpcStates
    {
        Walking,
        DefensePositon
    }


    private void ChangeState()
    {
        if (this._currentState == NpcStates.Walking)
        {
            this._currentState = NpcStates.DefensePositon;
            StartCoroutine(CallAfterTime());
        }
        else
        {
            this._currentState = NpcStates.Walking;
            PositionGraphNode nTarget = this._walkingPosition.GetRandomPositionFromGraph();
            this._walkingPosition = nTarget;

            this._onTarget = false;
        }
    }
    IEnumerator CallAfterTime()
    {
        yield return new WaitForSeconds(5f);
        ChangeState();
    }
}

public enum NpcWeaponType
{
    FreeWalking,
    Defense,
    Escort
}
