using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperZombieController : MonoBehaviour
{
    public static SuperZombieController Instance;

    [SerializeField]
    private List<GameObject> _superZombiePrefabs;

    [SerializeField]
    private List<WalkingZombieChunk> _chunks;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        RaisZombies();
    }

    public void RaisZombies()
    {
        if (this._superZombiePrefabs == null || !MidleZombieController.Instance.CanSpawnHighZombie)
            return;

        for (int i = 0; i < this._chunks.Count; i++)
        {
            if (this._chunks[i].CheckAvailabilityPlayersInChunk())
            {
                for (int j = 0; j < this._superZombiePrefabs.Count; j++)
                {
                    SpawnZombie(this._chunks[i].GetRandomPositionFromGraph(), MidleZombieType.FatZombie, this._superZombiePrefabs[j]);
                }
            }
        }
    }

    private void SpawnZombie(PositionGraphNode position, MidleZombieType type, GameObject zombieName) 
    {
        MidleZombieAI zombie = Instantiate( zombieName, position.transform.position, position.transform.rotation).GetComponent<MidleZombieAI>();

        zombie.ConfigureZombie(type, position);
        zombie.OnMidleZombieDieWithParam += RegisterOfZombieDeth;
    }
    private void RegisterOfZombieDeth(int killedZombieType, bool isKilledByPlayer) { }
}
