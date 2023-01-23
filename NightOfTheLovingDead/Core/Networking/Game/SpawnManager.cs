using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField]
    private List<Transform> spawnPositions;

    [SerializeField]
    private List<SpawnPositionContainer> containers;

    [SerializeField]
    private float minRndOffset;
    [SerializeField]
    private float maxRndOffset;

    [SerializeField]
    private bool useRandomOffset;

    public List<Transform> SpawnPositions { get => spawnPositions; set => spawnPositions = value; }
    public float MinRndOffset { get => minRndOffset; set => minRndOffset = value; }
    public float MaxRndOffset { get => maxRndOffset; set => maxRndOffset = value; }
    public bool UseRandomOffset { get => useRandomOffset; set => useRandomOffset = value; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform GetSpawnpoint()
    {
        return GetSpawnPointFromList(this.spawnPositions);
    }

    private Transform GetSpawnPointFromList(List<Transform> pointsList)
    {
        int randomIndex = Random.Range(0, pointsList.Count);
        Transform spawnpoint = pointsList[randomIndex];
        
        if (this.UseRandomOffset)
            spawnpoint.position = new Vector3(spawnpoint.position.x + Random.Range(this.MinRndOffset, MaxRndOffset), spawnpoint.position.y, spawnpoint.position.z + Random.Range(this.MinRndOffset, MaxRndOffset));

        return spawnpoint;
    }

    private void RemoveSpawnPointFromItem(List<Transform> pointsList, Transform target)
    {
        pointsList.Remove(target);
    }

    
    public Transform GetSpawnPositionByTag(string tag, bool unique)
    {
        foreach (SpawnPositionContainer item in this.containers)
        {
            if (item.tag == tag)
            {
                Transform pos = GetSpawnPointFromList(item.spawnPositions);
                //if (unique)
                //    RemoveSpawnPointFromItem(item.spawnPositions,pos);

                return pos;
            }
        }

        return this.transform;
    }

    [System.Serializable]
    public struct SpawnPositionContainer
    {
        public string tag;
        public List<Transform> spawnPositions;
    }
}
