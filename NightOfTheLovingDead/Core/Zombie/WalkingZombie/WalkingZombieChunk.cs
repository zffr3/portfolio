using System.Collections.Generic;
using UnityEngine;

public class WalkingZombieChunk : MonoBehaviour, IZombieChunk
{
    [SerializeField]
    private GameObject _zombiePrefab;

    [SerializeField]
    private List<PositionGraphNode> _positions; 

    [SerializeField]
    private int _maxZombieToSpawn;

    [SerializeField]
    private int _spawnedZombieCount;

    [SerializeField]
    private List<GameObject> _playersInChunk;

    [SerializeField]
    private PoolAggregator _zombiePool;

    public event System.Action<string> AllPlayersExitChunk;

    public void SpawnZombie()
    {
        if (this._spawnedZombieCount == this._maxZombieToSpawn)
            return;

        Transform position = this._positions[Random.Range(0, this._positions.Count)].gameObject.transform;
        position.position += new Vector3(Random.Range(-0.5f,3.5f),0, Random.Range(-0.5f, 3.5f));

        Quaternion randomRotation = new Quaternion(0, Random.Range(60,360), 0,0);

        WalkingZombieAI zSrc = this._zombiePool.GetObjectFromPool<WalkingZombieAI>();

        zSrc.transform.position = position.position;
        zSrc.transform.Rotate(randomRotation.x, randomRotation.y, randomRotation.z);
        zSrc.gameObject.SetActive(true);
        zSrc.ConfigureZombie(this, position.gameObject.GetComponent<PositionGraphNode>());

        this._spawnedZombieCount++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Human>() != null && other.GetComponent<Human>().CurrentType != HumanType.NPC)
        {
            if (this._playersInChunk == null)
                this._playersInChunk = new List<GameObject>();

            this._playersInChunk.Add(other.gameObject);

            if (this._spawnedZombieCount < this._maxZombieToSpawn)
            {
                for (int i = 0; i < _maxZombieToSpawn; i++)
                {
                    SpawnZombie();
                    this._spawnedZombieCount++;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Human>() != null && other.GetComponent<Human>().CurrentType != HumanType.NPC &&  this._playersInChunk.Count != 0 )
        {
            this._playersInChunk.Remove(other.gameObject);

            if (this._playersInChunk.Count == 0)
                this.AllPlayersExitChunk?.Invoke("0xKilledByExitFromChunk");
        }
    }

    public bool CheckAvailabilityPlayersInChunk()
    {
        return this._playersInChunk.Count > 0;
    }

    public PositionGraphNode GetRandomPositionFromGraph()
    {
        return this._positions[Random.Range(0,this._positions.Count)];
    }

    private void RPC_SyncSpawnedZombieCoun(int newZombieCount)
    {
        this._spawnedZombieCount = newZombieCount;
    }

    public void OnZombieDie()
    {
        this._spawnedZombieCount -= 2;

        if (this._spawnedZombieCount < 0)
            this._spawnedZombieCount = 0;
    }
}

public interface IZombieChunk
{
    event System.Action<string> AllPlayersExitChunk;
    void OnZombieDie();
}