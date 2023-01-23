using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombieWeapon : MonoBehaviour, IZombieWeapon
{
    [SerializeField]
    private GameObject _spawnObject;

    [SerializeField]
    private List<Transform> _spawnPositions;

    [SerializeField]
    private int _spawnCount;

    [SerializeField]
    private int _spawnedCount;

    private Color _colorMarker;

    private void Start()
    {
        this._colorMarker = Color.yellow;
        EventBus.SubscribeToEvent(EventType.REACTIVE_ZOMBIE_KILLED, RegistrZombieDeth);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.REACTIVE_ZOMBIE_KILLED, RegistrZombieDeth);
    }

    public void Attack(Human target)
    {
        if (this._spawnedCount > 0)
        {
            return;
        }

        this._spawnedCount = this._spawnCount;
        for (int i = 0; i < this._spawnCount; i++)
        {
            Transform position = this._spawnPositions[Random.Range(0, this._spawnPositions.Count)].gameObject.transform;
            position.position += new Vector3(Random.Range(-0.5f, 2), 0, Random.Range(-0.5f, 2));

            GameObject zombie = Instantiate(this._spawnObject, position.position, position.rotation);
            zombie.GetComponent<ReactiveZombie>().ConfigureZombie(target.gameObject, 0.5f, 25f, Color.yellow, false);
        }
    }

    private void RegistrZombieDeth(object sender, object param)
    {
        if (this._colorMarker == (Color)param)
        {
            this._spawnedCount--;
        }
    }

    public void ConfigureWeapon(float attackDistance, float damage)
    {
        
    }
}
