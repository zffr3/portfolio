using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindQuest : MonoBehaviour
{
    public static FindQuest Instance;


    [SerializeField]
    private List<GameObject> _objectsToFinding;

    [SerializeField]
    private List<ItemToSearch> _searchItem;
    
    [SerializeField]
    private int _reward;

    private bool _isQuestStarted;

    public event System.Action OnQuestEnd;

    private void Start()
    {
        this._isQuestStarted = false;
        Instance = this;
    }

    public void StartQuest(string plrName,string param)
    {
        if (this._isQuestStarted)
            return;

        PlayerUI.Instance.SetQuestText("Find target");
        this._isQuestStarted = true;
        CreateItem(plrName);
    }

    public void EndQuest(ItemToSearch item)
    {
        this._isQuestStarted = false;
        PlayerUI.Instance.CloseQuestText();
        DestroyFindedItem(item);
        NetworkPlayer.NetworkPlayerInstance.TakeReward(this._reward);

        this.OnQuestEnd?.Invoke();
    }

    private bool CanTakeQuest(string plrName)
    {
        if (this._searchItem == null)
        {
            this._searchItem = new List<ItemToSearch>();
            return true;
        }
        for (int i = 0; i < this._searchItem.Count; i++)
            if (this._searchItem[i].GetPlrName() == plrName)
                return false;
        return true;
    }

    private void CreateItem(string plrName)
    {
        if (!CanTakeQuest(plrName))
            return;

        if (this._objectsToFinding == null)
            this._objectsToFinding = new List<GameObject>();

        Transform spawnPos = SpawnManager.Instance.GetSpawnPositionByTag("FindItemPositions", false);
        GameObject item =  Instantiate(this._objectsToFinding[Random.Range(0, this._objectsToFinding.Count)], spawnPos.position, spawnPos.rotation);

        ItemToSearch itmSrc = item.GetComponent<ItemToSearch>();
        itmSrc.ConfigureItem(plrName);

        this._searchItem.Add(itmSrc);
    }

    private void DestroyFindedItem(ItemToSearch itm)
    {
        this._searchItem.Remove(itm);
        GameObject.Destroy(itm.gameObject);
    }
}
