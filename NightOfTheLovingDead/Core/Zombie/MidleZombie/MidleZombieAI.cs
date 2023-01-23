using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MidleZombieAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _deathObject;

    [SerializeField]
    private GameObject[] _skins;

    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private ZombieSens _sens;

    [SerializeField]
    private WalkinZombieAnimation _animation;

    [SerializeField]
    private IZombieWeapon _weapon;

    [SerializeField]
    private float _attackDistance;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _targetDelay;

    [SerializeField]
    private bool _useCustomWeapon;
    [SerializeField]
    private bool _useSkin;

    private PositionGraphNode _graphPosition;

    private GameObject _target;

    private Human _plrTarget;

    private bool _isPlayerTarget;

    [SerializeField]
    private int _zombieType;

    public event System.Action OnMidleZombieDie;
    public event System.Action<int,bool> OnMidleZombieDieWithParam;

    public void ConfigureZombie(MidleZombieType type,  PositionGraphNode position)
    {
        this._agent = GetComponent<NavMeshAgent>();
        this._sens = this.GetComponentInChildren<ZombieSens>();
        this._animation = this.GetComponent<WalkinZombieAnimation>();

        this._weapon = this.GetComponent<IZombieWeapon>();
        this._weapon.ConfigureWeapon(this._attackDistance, this._damage);

        this._zombieType = (int)type - 1;

        this.GetComponent<MidleZombieHealth>().OnZombieDie += CallDestroyZombie;

        this._graphPosition = position;
        StartCoroutine(GetDestinationPosition());

        this._sens.FindTarget += SetPlayerAsTarget;
        this._sens.LostPlayer += LostPlayer;

        if (this._useCustomWeapon && this._weapon != null)
        {
            this._weapon.ConfigureWeapon(this._attackDistance * ((float)type / 10), this._damage * ((float)type / 10));
        }

        this._speed -= this._speed * ((float)type / 10);

        if (this._useSkin)
        {
            this._skins[this._zombieType].SetActive(true);
        }

    }

    private void Update()
    {
        if (this._agent.isOnNavMesh)
        {
            if (this._target != null && !this._isPlayerTarget)
            {
                this._agent.SetDestination(this._target.transform.position);
            }

            if (this._plrTarget != null && this._isPlayerTarget)
            {
                this._agent.SetDestination(this._plrTarget.transform.position);
            }
        }

        if (this._target == null && this._isPlayerTarget)
        {
            LostPlayer();
        }


        this._animation.SetSpeed(Mathf.Abs(this._agent.velocity.magnitude));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this._isPlayerTarget && this._plrTarget != null)
        {
            if (Vector3.Distance(this.transform.position, this._plrTarget.transform.position) <= this._attackDistance)
            {
                this._weapon.Attack(this._plrTarget);
            }
        }   
    }

    private void LostPlayer()
    {
        this._isPlayerTarget = false;
        this._sens.SetTargetState(false);
    }

    private void SetPlayerAsTarget(GameObject player)
    {
        this._isPlayerTarget = true;
        this._plrTarget = player.GetComponent<Human>();
    }

    private IEnumerator GetDestinationPosition()
    {
        if (!this._isPlayerTarget)
        {
            this._target = this._graphPosition.gameObject;
            this._graphPosition = this._graphPosition.GetRandomPositionFromGraph();

            yield return new WaitForSeconds(this._targetDelay);

            StartCoroutine(GetDestinationPosition());
        }
    }

    public void CallDestroyZombie(string plrName)
    {
        if (plrName != "NPC")
        {
            NoldGameLoop.Instance.GiveReward(plrName, 500);
        }
        OnMidleZombieDie?.Invoke();
        OnMidleZombieDieWithParam?.Invoke(this._zombieType, plrName != "NPC");

        OnMidleZombieDie = null;
        OnMidleZombieDieWithParam = null;

        //DestroyZombie(plrName);
        PoolAggregator.Instance.ReturnObjectToPool(this);
        this.gameObject.SetActive(false);
    }

    private void DestroyZombie(string plrName)
    {
        Instantiate(this._deathObject, this.transform.position + Vector3.up, this.transform.rotation);
        GameObject.Destroy(this.gameObject);
    }
}
public enum MidleZombieType
{
    SkinnyZombie = 1,
    FatPrincessZombie = 2,
    FatZombie = 3
}
