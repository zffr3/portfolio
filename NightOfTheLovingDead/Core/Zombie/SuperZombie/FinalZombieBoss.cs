using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZombieBoss : MonoBehaviour
{
    [SerializeField]
    private IZombieWeapon _meleWeapon;
    [SerializeField]
    private IZombieWeapon _balisticWeapon;
    [SerializeField]
    private IZombieWeapon _spawnWeapon;

    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Human _humanSrc;

    [SerializeField]
    private float _criticalPlayerDistance;
    [SerializeField]
    private float _throwAndSpawnDistance;
    [SerializeField]
    private float _meleAttackDistance;

    [SerializeField]
    private Transform _currentPosition;

    [SerializeField]
    private bool _canChangePosition;
    [SerializeField]
    private float _canChangePositionCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        this._canChangePosition = true;

        this._meleWeapon = this.GetComponent<DefaultZombieWeapon>();
        this._balisticWeapon = this.GetComponent<ThrowWeapon>();
        this._spawnWeapon = this.GetComponent<SpawnZombieWeapon>();

        this._player = NetworkPlayer.NetworkPlayerInstance.GetCharacterTransform().gameObject;
        this._humanSrc = this._player.GetComponent<Human>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPositionAndAttack();
        LookToPlayer();
    }

    private void CheckPositionAndAttack()
    {
        if (this._player != null)
        {
            float distanceToPlayer = Vector3.Distance(this.transform.position, this._player.transform.position);
            if (distanceToPlayer < this._criticalPlayerDistance)
            {
                ChangePosition();
            }
            else if (distanceToPlayer < this._throwAndSpawnDistance)
            {
                this._spawnWeapon.Attack(this._humanSrc);
                this._balisticWeapon.Attack(this._humanSrc);
            }
            else if (distanceToPlayer < this._meleAttackDistance)
            {
                this._meleWeapon.Attack(this._humanSrc);
            }
        }
    }

    private void ChangePosition()
    {
        if (this._canChangePosition)
        {
            this._canChangePosition = false;
            StartCoroutine(ResetCanCangePosition());

            Transform newPosition = this._currentPosition;
            do
            {
                 newPosition = SpawnManager.Instance.GetSpawnPositionByTag("TeleportPoints", false);
            } while (this._currentPosition == newPosition);

            this.transform.position = newPosition.position;
        }
    }

    private void LookToPlayer()
    {
        this.transform.LookAt(new Vector3(this._player.transform.position.x,this.transform.position.y,this._player.transform.position.z));
    }

    IEnumerator ResetCanCangePosition()
    {
        yield return new WaitForSeconds(this._canChangePositionCoolDown);
        this._canChangePosition = true;
    }
}
