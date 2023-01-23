using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneToClean : MonoBehaviour
{
    [SerializeField]
    private GameObject _wallsObject;

    [SerializeField]
    private GameObject _highLighter;

    [SerializeField]
    private List<Transform> _zombieSpawnPositions;

    [SerializeField]
    private List<Transform> _crateSpawnPositions;

    [SerializeField]
    private GameObject _zombiePrefab;

    [SerializeField]
    private GameObject _cratePrefab;

    [SerializeField]
    private WeaponData _defaultWeapon;

    [SerializeField]
    private int _zombieCount;

    [SerializeField]
    private int _spawnedZombieCount;

    [SerializeField]
    private int _crateCount;

    private bool _isStarted;

    private GameObject _player;

    private Color _colorMarker;

    // Start is called before the first frame update
    void Start()
    {
        this._spawnedZombieCount = 0;
        this._colorMarker = Color.black;
        EventBus.SubscribeToEvent(EventType.REACTIVE_ZOMBIE_KILLED, RegistrZombieDeth);
    }

    private void OnDisable()
    {
        EventBus.UnsubscribeFromEvent(EventType.REACTIVE_ZOMBIE_KILLED, RegistrZombieDeth);
    }

    // Update is called once per frame
    void Update()
    {
        if (this._isStarted && this._player == null)
        {
            RestartQuest();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NetworkCharacter character = other.GetComponent<NetworkCharacter>();
        if (character != null &&
            NetworkPlayer.NetworkPlayerInstance.IsEqualNickName(character.GetPlayerName()) &&
            !this._isStarted)
        {
            this._player = other.gameObject;
            StartQuest();
        }
    }

    private void StartQuest()
    {
        this._highLighter.SetActive(false);

        this._isStarted = true;

        this._wallsObject.SetActive(true);
        this.GetComponent<BoxCollider>().enabled = false;

        SpawnCrate();

        SpawnZombies();
        PlayerUI.Instance.SetQuestText($"Kill all zombies: {this._spawnedZombieCount}");

        PoolAggregator.Instance.ChangeActiveObjectState<WalkingZombieAI>(false);
        PoolAggregator.Instance.ChangeActiveObjectState<MidleZombieAI>(false);
    }

    private void RestartQuest()
    {
        this._isStarted = false;

        this._wallsObject.SetActive(false);
        this.GetComponent<BoxCollider>().enabled = true;

        this._spawnedZombieCount = 0;

        PlayerUI.Instance.CloseQuestText();
    }

    private void EndQuest()
    {
        this._isStarted = false;

        this._wallsObject.SetActive(false);
        this.GetComponent<BoxCollider>().enabled = true;

        PlayerUI.Instance.CloseQuestText();

        ZoneCleanerQuest.Instance.EndQuest();

        this._highLighter.SetActive(true);
        
        this.gameObject.SetActive(false);

        PoolAggregator.Instance.ChangeActiveObjectState<WalkingZombieAI>(false);
        PoolAggregator.Instance.ChangeActiveObjectState<MidleZombieAI>(false);
    }

    private void SpawnCrate()
    {
        for (int i = 0; i < this._crateCount; i++)
        {
            Transform spawnPos = this._crateSpawnPositions[Random.Range(0, this._crateSpawnPositions.Count)];
            spawnPos.position += new Vector3(Random.Range(-0.5f, 2), 0, Random.Range(-0.5f, 2));

            GameObject crate = Instantiate(this._cratePrefab, spawnPos.position, spawnPos.rotation);

            crate.GetComponent<DroppedWeapon>().SetDroppedWeapon(this._defaultWeapon.GetWeaponName());
        }
    }

    private void SpawnZombies()
    {
        this._spawnedZombieCount = this._zombieCount;
        for (int i = 0; i < this._zombieCount; i++)
        {
            Transform position = this._zombieSpawnPositions[Random.Range(0, this._zombieSpawnPositions.Count)].gameObject.transform;
            position.position += new Vector3(Random.Range(-0.5f, 2), 0, Random.Range(-0.5f, 2));

            GameObject zombie = Instantiate(this._zombiePrefab, position.position, position.rotation);
            zombie.GetComponent<ReactiveZombie>().ConfigureZombie(this._player, 0.25f, 35f, Color.black, false);
        }
    }

    private void RegistrZombieDeth(object sender, object param)
    {
        if (this._colorMarker != (Color)sender)
        {
            return;
        }

        this._spawnedZombieCount--;

        PlayerUI.Instance.SetQuestText($"Kill all zombies: {this._spawnedZombieCount}");

        if (_spawnedZombieCount == 0)
            EndQuest();
    }

}