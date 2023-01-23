using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : MonoBehaviour
{
    public static ZombiePool Instance { get; private set; }

    [SerializeField]
    private int _poolSize;

    [SerializeField]
    private GameObject _zombiePrefab;

    [SerializeField]
    private List<WalkingZombieAI> _zombiePool;

    [SerializeField]
    private List<WalkingZombieAI> _zombiesInAction;

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        this._zombiePool = new List<WalkingZombieAI>();
        SpawnZombies();
    }

    private void SpawnZombies()
    {
        for (int i = 0; i < this._poolSize; i++)
        {
            WalkingZombieAI zTemp = Instantiate(this._zombiePrefab, Vector3.zero, Quaternion.identity).GetComponent<WalkingZombieAI>();
            zTemp.gameObject.SetActive(false);

            this._zombiePool.Add(zTemp);
        }
    }

    public WalkingZombieAI GetZombieFromPool()
    {
        if (this._zombiesInAction == null)
            this._zombiesInAction = new List<WalkingZombieAI>();

        if (this._zombiePool == null || this._zombiePool.Count == 0)
            InitializePool();

        WalkingZombieAI zombie = this._zombiePool[Random.Range(0, this._zombiePool.Count)];
        this._zombiesInAction.Add(zombie);

        return zombie;
    }

    public void ReturnZombieToPool(WalkingZombieAI zombie)
    {
        if (this._zombiesInAction == null)
            return;

        this._zombiesInAction.Remove(zombie);
        this._zombiePool.Add(zombie);
    }

    public void ChangeActiveZombieState(bool nState)
    {
        for (int i = 0; i < this._zombiesInAction.Count; i++)
            this._zombiesInAction[i].gameObject.SetActive(nState);
    }
}
