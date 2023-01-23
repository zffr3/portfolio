using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBeehive : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _honeycombsSpawnPositions;
    [SerializeField]
    private List<GameObject> _honeycombsSpawnObjects;
    [SerializeField]
    public int _honeycombCount;

    //Its zombie prefab
    [SerializeField]
    private GameObject _beePrefab;
    [SerializeField]
    private int _beeCount;

    [SerializeField]
    private int _maxBeeCount;
    [SerializeField]
    private int _spawnedBeeCount;

    [SerializeField]
    private bool _isBeehiveClosed;

    private List<GameObject> _characterList;

    private Color _colorMarker;

    [SerializeField]
    private bool _useWave;
    [SerializeField]
    private int _waveCount;
    [SerializeField]
    private int _currentWave;
    [SerializeField]
    private int _beeInWaveStart;
    [SerializeField]
    private int _beeInWaveMax;

    private void Start()
    {
        this._colorMarker = Color.red;
        EventBus.SubscribeToEvent(EventType.REACTIVE_ZOMBIE_KILLED, RegistrBeeDeath);
    }

    public void AcceptPlayerToBeehive(GameObject character)
    {
        if (this._isBeehiveClosed)
        {
            return;
        }
        if (this._characterList == null)
        {
            this._characterList = new List<GameObject>();
        }

        this._characterList.Add(character);
        StirUpTheBeehive();
    }

    public void RaisPlayerFromBeehive(GameObject character)
    {
        if (this._characterList == null)
        {
            return;
        }

        this._characterList.Remove(character);
    }


    private void StirUpTheBeehive()
    {
        if (this._maxBeeCount <= 0)
        {
            return;
        }

        if (this._useWave)
        {
            StartWavedBeeSpawn();
        }
        else
        {
            StartSimpleBeeSpawn();
        }
    }

    private void RegistrBeeDeath(object sender, object param)
    {
        if (this._colorMarker != (Color)param)
        {
            return;
        }

        this._spawnedBeeCount--;

        if (this._useWave)
        {
            this._currentWave++;
            if (this._currentWave < this._waveCount)
            {
                this._beeCount = Mathf.Clamp(this._beeInWaveStart * this._currentWave,1, this._beeInWaveMax);
                StartSimpleBeeSpawn();
            }

            if (this._currentWave >= this._waveCount)
            {
                CloseBeehive();
            }
        }
        else
        {
            this._maxBeeCount--;

            if (this._spawnedBeeCount <= 0)
            {
                StartSimpleBeeSpawn();
            }

            if (this._maxBeeCount <= 0)
            {
                CloseBeehive();
            }
        }
    }

    private void CloseBeehive()
    {
        this._isBeehiveClosed = true;
        EventBus.UnsubscribeFromEvent(EventType.REACTIVE_ZOMBIE_KILLED, RegistrBeeDeath);
        SpawnHoneycomb();
    }

    private void SpawnHoneycomb()
    {
        for (int i = 0; i < this._honeycombCount; i++)
        {
            Transform spawnPos = this._honeycombsSpawnPositions[Random.Range(0, this._honeycombsSpawnPositions.Count)];
            PhotonNetwork.Instantiate(this._honeycombsSpawnObjects[Random.Range(0, this._honeycombsSpawnObjects.Count)].name, spawnPos.position, spawnPos.rotation);
        }
    }

    private void StartSimpleBeeSpawn()
    {
        this._spawnedBeeCount += this._beeCount;

        for (int i = 0; i < this._beeCount; i++)
        {
            SpawnBee();
        }
    }

    private void StartWavedBeeSpawn()
    {
        this._currentWave = 1;

        this._beeCount = this._beeInWaveStart * this._currentWave;
        StartSimpleBeeSpawn();
    }

    private void SpawnBee()
    {
        Transform position = this._honeycombsSpawnPositions[Random.Range(0, this._honeycombsSpawnPositions.Count)].gameObject.transform;
        position.position += new Vector3(Random.Range(-0.5f, 2), 0, Random.Range(-0.5f, 2));

        GameObject zombie = Instantiate(this._beePrefab, position.position, position.rotation);
        zombie.GetComponent<ReactiveZombie>().ConfigureZombie(this._characterList[Random.Range(0, this._characterList.Count)], 1f, 100f, Color.red, false);
    }
}
