using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCleanerQuest : MonoBehaviour
{
    public static ZoneCleanerQuest Instance;

    [SerializeField]
    private List<ZoneToClean> _zones;

    [SerializeField]
    private List<ZoneQuestContainer> _zonesDictionary;
    private List<string> _busyKeys;


    private List<int> _busyIndexes;

    [SerializeField]
    private int _minReward;

    [SerializeField]
    private int _maxReward;

    private bool _isQuestStarted;
    private string _questId;

    public event System.Action OnQuestEnd;

    private void Awake()
    {
        Instance = this;

        if (this._zones == null)
            this._zones = new List<ZoneToClean>();

        if (this._busyIndexes == null)
            this._busyKeys = new List<string>();

        this._isQuestStarted = false;
    }

    public void StartQuest(string plrName, string key)
    {
        if (this._isQuestStarted)
            return;

        this._isQuestStarted = true;

        if (this._busyKeys.Contains(key))
            return;

        ZoneToClean zoneToClean = null;
        for (int i = 0; i < this._zonesDictionary.Count; i++)
        {
            if (this._zonesDictionary[i].QuestId == key)
            {
                zoneToClean = this._zonesDictionary[i].Zone;
                break;
            }
        }

        if (zoneToClean == null)
            return;

        zoneToClean.gameObject.SetActive(true);
        SyncIndex(key, true);

        this._questId = key;
    }

    public void EndQuest()
    {
        SyncIndex(this._questId, false);
        this._isQuestStarted = false;

        this.OnQuestEnd?.Invoke();

        NetworkPlayer.NetworkPlayerInstance.TakeReward(Random.Range(this._minReward,this._maxReward));
    }

    private void SyncIndex(string key, bool action)
    {
        if (this._busyKeys == null)
            this._busyKeys = new List<string>();

        if (action)
            this._busyKeys.Add(key);
        else
            this._busyKeys.Remove(key);
    }
}

[System.Serializable]
public class ZoneQuestContainer
{
    [SerializeField]
    public string QuestId;

    [SerializeField]
    public ZoneToClean Zone;
}
