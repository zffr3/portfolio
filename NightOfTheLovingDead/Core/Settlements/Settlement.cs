using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    [SerializeField]
    private int _maxCivilianCount;
    [SerializeField]
    private int _spawnCivilianCount;

    [SerializeField]
    private List<GameObject> _civilians;
    [SerializeField]
    private List<Transform> _civiliansPositions;
    [SerializeField]
    private List<GameObject> _spawnedCivilians;

    [SerializeField]
    private List<Transform> _hordeSpawnPoints;
    [SerializeField]
    private GameObject _hordeZombie;
    [SerializeField]
    private int _hordeZombieCount;

    [SerializeField]
    private bool _isSettlementActive;

    [SerializeField]
    private int _hordeRatio;

    [SerializeField]
    private int _suppliesRatio;

    // Start is called before the first frame update
    void Start()
    {
        this._spawnedCivilians = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
        {
            return;
        }

        if (this._isSettlementActive && other.GetComponent<Human>().CurrentType == HumanType.Player)
        {
            other.GetComponent<IDamagble>().AddHealth(100);
            other.GetComponent<PlayerWeapon>().AddAmmoToWeapon(100);
        }
    }

    public void ActivateSettlement()
    {
        this._isSettlementActive = true;
        this._hordeRatio += 10;
        SpawnNewCivilian();
    }

    public void DropSettlement()
    {
        if (!this._isSettlementActive)
        {
            return;
        }

        this._hordeRatio -= 5;
        if (this._hordeRatio <= 0)
        {
            RaisHorde();
        }

        this._suppliesRatio -= 5;
        if (this._suppliesRatio <= 0)
        {
            KillCiviliansByHungry();
        }
    }

    public void AddSuplies()
    {
        this._suppliesRatio += 10;
    }

    private void SpawnNewCivilian()
    {
        if (this._spawnedCivilians.Count >= this._maxCivilianCount)
        {
            return;
        }

        for (int i = 0; i < this._spawnCivilianCount; i++)
        {
            Transform spawnPosition = GetRandomPoint();

            GameObject nCivilian = Instantiate(this._civilians[Random.Range(0, this._civilians.Count)], spawnPosition.position, spawnPosition.rotation);

            nCivilian.GetComponent<INpc>().Configure(GetRandomPoint(), this, RegisterCivilianDeath);

            this._spawnedCivilians.Add(nCivilian);
        }
    }

    private void RaisHorde()
    {
        if (this._civilians == null && this._civilians.Count == 0)
        {
            return;
        }

        this._isSettlementActive = false;
        for (int i = 0; i < this._hordeZombieCount; i++)
        {
            int indP = Random.Range(0, this._hordeSpawnPoints.Count);
            Transform position = this._hordeSpawnPoints[indP].gameObject.transform;
            position.position += new Vector3(Random.Range(-0.5f, 2), 0, Random.Range(-0.5f, 2));

            int indC = Random.Range(0, this._spawnedCivilians.Count);
            GameObject zombie = Instantiate(this._hordeZombie, position.position, position.rotation);
            zombie.GetComponent<ReactiveZombie>().ConfigureZombie(this._spawnedCivilians[indC], 0.75f,50f, Color.blue, false);
        }
    }

    private void KillCiviliansByHungry()
    {
        int civiliansCount = Random.Range(1,  this._spawnedCivilians.Count);

        for (int i = 0; i < civiliansCount; i++)
        {
            this._spawnedCivilians[Random.Range(0, this._spawnedCivilians.Count)].GetComponent<IDamagble>().TakeDamage(100, "Hungry");
        }

        if (this._spawnedCivilians.Count <= 0)
        {
            this._isSettlementActive = false;
        }
    }

    private void RegisterCivilianDeath(GameObject civilian)
    {
        this._spawnedCivilians.Remove(civilian);
    }

    public Transform GetRandomPoint()
    {
        return this._civiliansPositions[Random.Range(0, this._civiliansPositions.Count)];
    }
}
