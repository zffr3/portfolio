using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(RZHealth),typeof(NavMeshAgent))]
public class ReactiveZombie : MonoBehaviour
{
    [SerializeField]
    private GameObject _defaultSkin;
    [SerializeField]
    private GameObject[] _skins;
    [SerializeField]
    private bool _useSkin;

    [SerializeField]
    private float _attackDistance;
    [SerializeField]
    private Human _target;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Outline _outlineSrc;

    [SerializeField]
    private float _damage;

    private bool _hasTarget;

    [SerializeField]
    private GameObject _deathObject;

    [SerializeField]
    private DefaultZombieWeapon _weapon;

    private Color _currentColorMarker;

    void Update()
    {
        if (this._target == null || this._target.gameObject.activeSelf == false)
        {
            if (this._hasTarget)
            {
                this._hasTarget = false;
                CallDestroyZombie(false);
            }
        }
        if (this._target != null && this._agent != null)
        {
            if (this._agent.isOnNavMesh)
            {
                this._agent.SetDestination(this._target.transform.position);
            }
        }
    }

    private void FixedUpdate()
    {
        if (this._target != null && this._agent != null)
        {
            if (Vector3.Distance(this.transform.position, this._target.transform.position) < this._attackDistance)
            {
                this._weapon.Attack(this._target);
            }
        }
    }

    public void ConfigureZombie(GameObject player, float damage, float health, Color nColor, bool skinned)
    {
        this._outlineSrc = this.GetComponentInChildren<Outline>();
        this._agent = this.GetComponent<NavMeshAgent>();
        this._animator = this.GetComponentInChildren<Animator>();
        this._target = player.GetComponent<Human>();
        this._hasTarget = true;

        float speed = Random.Range(1, 7);
        this._agent.speed = speed;

        this._damage = damage;
        this._weapon.ConfigureWeapon(this._attackDistance, this._damage);
        this.GetComponent<IDamagble>().AddHealth(health);

        if (this._useSkin)
        {
            this._skins[Random.Range(0, this._skins.Length)].SetActive(true);
        }
        else
        {
            this._defaultSkin.SetActive(true);
        }

        this._currentColorMarker = nColor;

        this._outlineSrc.SetColor(nColor);
        this._animator.SetBool("Walking", true);
    }

    public void CallDestroyZombie(bool killedByPlayer)
    {
        Instantiate(this._deathObject, this.transform.position + Vector3.up, this.transform.rotation);
        Destroy(this.gameObject);
        if (killedByPlayer)
        {
            EventBus.Dispath(EventType.REACTIVE_ZOMBIE_KILLED, this, this._currentColorMarker);
        }
    }
}
