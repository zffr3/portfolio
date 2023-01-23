using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidleZombieController : MonoBehaviour
{
    public static MidleZombieController Instance;

    [SerializeField]
    private GameObject _midleZombie;

    [SerializeField]
    private bool[] _killedZombies;

    [SerializeField]
    private List<WalkingZombieChunk> _chunks;

    [SerializeField]
    private int _minZombiesInChunk;
    [SerializeField]
    private int _maxZombieInChunk;

    [SerializeField]
    private int _maxSpawnedZombies;
    [SerializeField]
    private int _spawnedZombieCount;

    [SerializeField]
    private bool _canSpawnHighZombie;
    public bool CanSpawnHighZombie { get { return _canSpawnHighZombie;} }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        this._killedZombies = new bool[] { false, false, false };
        RaisZombies();
    }

    public void RaisZombies() 
    {
        if (this._spawnedZombieCount >= this._maxSpawnedZombies)
            return;

        for (int i = 0; i < this._chunks.Count; i++)
        {
            if (this._chunks[i].CheckAvailabilityPlayersInChunk())
            {
                int zombieCount = Random.Range(this._minZombiesInChunk, this._maxZombieInChunk + 1);
                for (int j = 0; j < zombieCount; j++)
                    SpawnZombie(this._chunks[i].GetRandomPositionFromGraph(), (MidleZombieType)Random.Range(1,4));
            }
        }
    }

    private void SpawnZombie(PositionGraphNode position, MidleZombieType type)
    {
        if (this._spawnedZombieCount >= this._maxSpawnedZombies)
            return;

        this._spawnedZombieCount++;


        MidleZombieAI zombie = PoolAggregator.Instance.GetObjectFromPool<MidleZombieAI>();
        zombie.transform.position = position.transform.position;
        zombie.gameObject.SetActive(true);

        //MidleZombieAI zombie = (Instantiate(this._midleZombie, position.transform.position, position.transform.rotation)).GetComponent<MidleZombieAI>();

        zombie.ConfigureZombie(type, position);
        zombie.OnMidleZombieDieWithParam += RegisterOfZombieDeth;
    }

    private void RegisterOfZombieDeth(int killedZombieType, bool isKilledByPlayer)
    {
        if (!isKilledByPlayer)
            return;

        this._killedZombies[killedZombieType-1] = true;
        this._spawnedZombieCount--;

        this._canSpawnHighZombie = this._killedZombies[0];
        for (int i = 0; i < this._killedZombies.Length; i++)
            this._canSpawnHighZombie &= this._killedZombies[i];
    }
}
