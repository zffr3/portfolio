using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingNpcController : MonoBehaviour
{
    public static WalkingNpcController instance;

    [SerializeField]
    private GameObject _npcPrefab;

    [SerializeField]
    private List<WalkingZombieChunk> _chunks;

    [SerializeField]
    private int _minNpcInChunk;
    [SerializeField]
    private int _maxNpcInChunk;

    [SerializeField]
    private int _maxSpawnedNpc;
    [SerializeField]
    private int _spawnedNpcCount;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnNpc()
    {
        if (this._spawnedNpcCount >= this._maxSpawnedNpc)
            return;

        for (int i = 0; i < this._chunks.Count; i++)
        {
            if (this._chunks[i].CheckAvailabilityPlayersInChunk())
            {
                int npcCount = Random.Range(this._minNpcInChunk, this._maxNpcInChunk);
                for (int j = 0; j < npcCount; j++)
                {
                    SpawnNpc(this._chunks[i].GetRandomPositionFromGraph());
                }
            }
        }
    }

    private void SpawnNpc(PositionGraphNode position)
    {
        if (this._spawnedNpcCount >= this._maxSpawnedNpc)
            return;

        this._spawnedNpcCount++;

        NpcWeaponAi npc = (Instantiate(this._npcPrefab,position.transform.position,position.transform.rotation)).GetComponent<NpcWeaponAi>();
        npc.ConfigureNpc(position);
    }

}
