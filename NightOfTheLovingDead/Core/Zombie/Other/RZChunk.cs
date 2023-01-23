using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RZChunk : MonoBehaviour
{
    [SerializeField]
    private GameObject _zombiePrefab;

    [SerializeField]
    private List<Transform> _spawnPositions;

    private List<GameObject> _players;

    // Start is called before the first frame update
    void Start()
    {
        this._players = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() != null)
            this._players.Add(other.gameObject);
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() != null)
            this._players.Remove(other.gameObject);
    }

    public void SpawnZombie()
    {
        Transform sp = this._spawnPositions[Random.Range(0, this._spawnPositions.Count)];
        GameObject zombie = Instantiate(this._zombiePrefab, sp.position, sp.rotation);
    }
}
