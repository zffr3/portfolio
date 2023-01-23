using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerController : MonoBehaviour
{
    public static BunkerController instance;

    [SerializeField]
    private List<WeaponData> _allWeapons;
    [SerializeField]
    private GameObject _weaponCrate;

    [SerializeField]
    private int _cratesCount;

    [SerializeField]
    private Animator _doorAnimations;

    [SerializeField]
    private GameObject _bossPrefab;

    [SerializeField]
    private GameObject _keyObject;

    private void Awake()
    {
        instance = this;
        SpawnCrates();
    }

    private void SpawnCrates()
    {
        for (int i = 0; i < this._cratesCount; i++)
        {
            Transform targetPos = SpawnManager.Instance.GetSpawnPositionByTag("BunkerCrates", true);
            GameObject crate = Instantiate(this._weaponCrate, targetPos.position, targetPos.rotation);
            crate.GetComponent<DroppedWeapon>().SetDroppedWeapon(this._allWeapons[Random.Range(0, this._allWeapons.Count)].GetWeaponName());
        }
    }

    public void OpenDoorQuest()
    {
        Transform keyPos = SpawnManager.Instance.GetSpawnPositionByTag("KeySpawnPositions", true);
        GameObject key = Instantiate(this._keyObject, keyPos.position, keyPos.rotation);
    }

    public void CloseDoorQuest()
    {
        Debug.Log("CloseDoorQuest");
        OpenTheDoor();
        SpawnBoss();
    }

    private void OpenTheDoor()
    {
        Debug.Log("OpenTheDoor");
        this._doorAnimations.SetTrigger("OpenTheDoor");
    }

    private void SpawnBoss()
    {
        Debug.Log("SpawnBoss");
        Transform spawnPos = SpawnManager.Instance.GetSpawnPositionByTag("TeleportPoints", true);
        GameObject boss = Instantiate(this._bossPrefab, spawnPos.position, spawnPos.rotation);
    }
}
