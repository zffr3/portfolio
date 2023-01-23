using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoldGameLoop : MonoBehaviour
{
    public static NoldGameLoop Instance;

    [SerializeField]
    private List<WeaponData> _allWeapons;

    [SerializeField]
    private int _currentCratelvl;

    [SerializeField]
    private List<WeaponData> _weaponsFromCrateLvl1;
    [SerializeField]
    private List<WeaponData> _weaponsFromCrateLvl2;
    [SerializeField]
    private List<WeaponData> _weaponsFromCrateLvl3;

    public event System.Action OnTimerEnd;

    [SerializeField]
    private GameObject _cratePrefab;

    [SerializeField]
    private List<GameObject> _allCrates;

    [SerializeField]
    private int _minCratesCount;
    [SerializeField]
    private int _maxCratesSount;

    [SerializeField]
    private List<NetworkPlayer> _players;

    [SerializeField]
    private List<Settlement> _settlements;

    [SerializeField]
    private float _time;

    private float _timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

            this._allCrates = new List<GameObject>();

            if (this._currentCratelvl == 0)
                this._allWeapons = this._weaponsFromCrateLvl1;
            else if (this._currentCratelvl == 1)
                this._allWeapons = this._weaponsFromCrateLvl2;
            else
                this._allWeapons = this._weaponsFromCrateLvl3;

            SpawnCrates();
            StartCoroutine(StartTimer());

            this.OnTimerEnd += SpawnCrates;
            this.OnTimerEnd += SpawnMidleZombies;
            this.OnTimerEnd += SpawnHighZombies;
            this.OnTimerEnd += UpdateZobmieStats;
            this.OnTimerEnd += SpawnHord;
            this.OnTimerEnd += SpawnNpc;
    }

    private void OnDestroy()
    {

            this.OnTimerEnd -= SpawnCrates;
            this.OnTimerEnd -= SpawnMidleZombies;
            this.OnTimerEnd -= SpawnHighZombies;
            this.OnTimerEnd -= UpdateZobmieStats;
            this.OnTimerEnd -= SpawnHord;
            this.OnTimerEnd -= SpawnNpc;
    }

    public void AddPlayerToList(NetworkPlayer plr)
    {
        if (this._players == null)
            this._players = new List<NetworkPlayer>();

        this._players.Add(plr);
    }

    public void RemovePlayerFromList(NetworkPlayer plr)
    {
        if (this._players == null)
            return;

        this._players.Remove(plr);
    }

    public void GiveReward(string plrName, int reward)
    {
        for (int i = 0; i < this._players.Count; i++)
        {
            if (this._players[i].IsEqualNickName(plrName))
                this._players[i].TakeReward(reward);
        }
    }

    private void SpawnCrates()
    {
            if (this._allCrates.Count != 0 )
            {
                for (int i = 0; i < this._allCrates.Count; i++)
                {
                    if (this._allCrates[i] != null)
                    {
                        this._allCrates[i].GetComponent<DroppedWeapon>().DesctroyCrate();
                    }
                }

                this._allCrates.Clear();
            }

            int currentCrateCount = Random.Range(this._minCratesCount, this._maxCratesSount);
           

            for (int i = 0; i < currentCrateCount; i++)
            {
                Transform targetPos = SpawnManager.Instance.GetSpawnPositionByTag("ZombieLandCrates", true);
                GameObject target = Instantiate(this._cratePrefab, targetPos.position, targetPos.rotation);
                target.GetComponent<DroppedWeapon>().SetDroppedWeapon(this._allWeapons[Random.Range(0,this._allWeapons.Count)].GetWeaponName());

                this._allCrates.Add(target);
            }
    }

    private void SpawnMidleZombies()
    {
        MidleZombieController.Instance.RaisZombies();
    }

    private void SpawnHighZombies()
    {
        SuperZombieController.Instance.RaisZombies();
    }

    private void SpawnNpc()
    {
        WalkingNpcController.instance.SpawnNpc();
    }

    private void UpdateZobmieStats()
    {
        WalkingZombieController.Instance.MultiplyValue();
    }

    private void SpawnHord()
    {
        for (int i = 0; i < this._settlements.Count; i++)
            this._settlements[i]?.DropSettlement();
    }

    private IEnumerator StartTimer()
    {
        while (this._timeLeft > 0)
        {
            this._timeLeft -= Time.deltaTime;
            //Do somthing when timer tick
            yield return null;
        }
        this.OnTimerEnd?.Invoke();

        this._timeLeft = this._time;
        StartCoroutine(StartTimer());
    }
}
