using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingZombieAI : CachedMonoBehaviour
{
    [SerializeField]
    private GameObject _deathObject;

    [SerializeField]
    private List<GameObject> _skins;

    [SerializeField]
    private NavMeshAgent _agent;
    private NavMeshPath _path;

    [SerializeField]
    private float _randomPointRadius;

    private Human _hTarget;

    [SerializeField]
    private WalkinZombieAnimation _animation;

    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private float _damage;

    [SerializeField]
    private bool _isRandomSpeed;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _attackDistance;
    
    [SerializeField]
    private float _minTargetDelay;
    [SerializeField]
    private float _maxTargetDelay;

    [SerializeField]
    private bool _useSkins;

    [SerializeField]
    private bool _isPlayerTarget;
    private bool _canAttack;

    [SerializeField]
    private int _reward;

    private ZombieSens _sens;
    private DefaultZombieWeapon _weapon;

    private PositionGraphNode _positionGraph;

    private IZombieChunk _parentChunk;

    // Start is called before the first frame update
    public void ConfigureZombie(IZombieChunk parentChunk, PositionGraphNode startNode)
    {
        this._positionGraph = startNode;
        this._parentChunk = parentChunk;
        this._agent = this.GetComponent<NavMeshAgent>();
        this._animation = this.GetComponentInChildren<WalkinZombieAnimation>();
        this._sens = this.GetComponentInChildren<ZombieSens>();

        this._weapon = this.GetComponent<DefaultZombieWeapon>();
        this._weapon.ConfigureWeapon(this._attackDistance, this._damage);

        this._path = new NavMeshPath();

        this._canAttack = true;

        this._sens.FindTarget += SetPlayerAsTarget;
        this._sens.LostPlayer += LostPlayer;

        float[] zombieStats = WalkingZombieController.Instance.GetZombieStatValues();
        if (zombieStats.Length == 5)
        {
            this._damage = zombieStats[0];
            this.GetComponent<WZombieHealts>().AddHealth(zombieStats[2]);
            this._reward = (int)zombieStats[3];
            this._sens.gameObject.transform.localScale = new Vector3(zombieStats[4], zombieStats[4], zombieStats[4]);
        }

        if (this._isRandomSpeed)
        {
            float speed = Random.Range(0.5f, 6);
            this._agent.speed = speed + zombieStats[1];
        }
        else
        {
            this._agent.speed = this._speed;
        }
        this._agent.avoidancePriority = Random.Range(0,1000);

        StartCoroutine(GetRandomState());

        if (this._useSkins)
        {
            int skinInd = Random.Range(0, this._skins.Count);
            this._skins[skinInd].SetActive(true);
        }

        this._parentChunk.AllPlayersExitChunk += CallDestroyZombie;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvent();
    }

    private void UnsubscribeFromEvent()
    {
        if (this._sens != null)
        {
            this._sens.FindTarget -= SetPlayerAsTarget;
            this._sens.LostPlayer -= LostPlayer;
        }

        if (this._parentChunk == null)
        {
            return;
        }


        this._parentChunk.AllPlayersExitChunk -= CallDestroyZombie;
    }

    public override void OnTick()
    {
        this.cachedTransform.Rotate(CalculateRotation()); 

        if (this._target != null && !this._isPlayerTarget)
        {
            this._agent.SetDestination(this._target.transform.position);
        }
        else if (this._hTarget != null && this._agent.isOnNavMesh)
        {
            this._agent.SetDestination(this._hTarget.transform.position);
        }

        if (this._target == null && this._isPlayerTarget)
        {
            LostPlayer();
        }


        this._animation.SetSpeed(Mathf.Abs(this._agent.velocity.magnitude));
    }

    Vector3 CalculateRotation()
    {
        Quaternion desiredRot = Quaternion.FromToRotation(this._agent.velocity, this._agent.desiredVelocity);
        float maxYAngleThisFrame = this._agent.angularSpeed * Time.deltaTime;
        float desiredYAngle = desiredRot.eulerAngles.y;
        if (desiredYAngle > 180) 
        {
            desiredYAngle -= 360; 
            
        } 
        
        if (Mathf.Abs(desiredYAngle) > maxYAngleThisFrame)
        {
            desiredRot.eulerAngles = new Vector3(desiredRot.eulerAngles.x, Mathf.Sign(desiredYAngle) * maxYAngleThisFrame, desiredRot.eulerAngles.z);
        }
        return desiredRot * this._agent.velocity;
    }

    void FixedUpdate()
    {
        if (this._isPlayerTarget && this._hTarget != null && this._canAttack)
        {
            this._weapon.Attack(this._hTarget);
        }

    }

    public void SetPlayerAsTarget(GameObject player)
    {
        if (this._isPlayerTarget)
        {
            return;
        }
        this._hTarget = player.GetComponent<Human>();

        DisableAllState();

        this._isPlayerTarget = true;
        this._target = this._hTarget.gameObject;
    }

    public void LostPlayer()
    {
        this._isPlayerTarget = false;

        DisableAllState();
        StartCoroutine(GetRandomState());

        this._sens.SetTargetState(false);
    }

    private void DisableAllState()
    {
        StopAllCoroutines();
        for (int i = 0; i < 3; i++)
            this._animation.SetZombieAnimationState((ZombieState)i, false);
    }

    public void CallDestroyZombie(string plrName)
    {
        if (plrName != "NPC")
        {
            NoldGameLoop.Instance.GiveReward(plrName, this._reward);
        }
        this._parentChunk.OnZombieDie();
        Instantiate(this._deathObject, this.cachedTransform.position + Vector3.up, this.cachedTransform.rotation);

        PoolAggregator.Instance.ReturnObjectToPool<WalkingZombieAI>(this);
        this.gameObject.SetActive(false);
    }

    IEnumerator GetRandomState()
    {
        if (!this._isPlayerTarget)
        {
            int state = Random.Range(0, 10);
            DisableAllState();
            this._animation.SetZombieAnimationState((ZombieState)Mathf.Clamp(state, 0, 2), true);

            if (state < 3)
            {
                this._target = this._positionGraph.gameObject;
                this._positionGraph = this._positionGraph.GetRandomPositionFromGraph();
            }
            else
            {
                this._target = null;
            }

            float delay = Random.Range(this._minTargetDelay, this._maxTargetDelay);
            yield return new WaitForSeconds(delay);
        }

        if (!this._isPlayerTarget)
        {
            StartCoroutine(GetRandomState());
        }
    }
}

public enum ZombieState
{
    Etc,
    Hurdle,
    Walk,
}
