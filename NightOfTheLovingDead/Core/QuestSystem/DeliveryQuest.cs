using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryQuest : MonoBehaviour
{
    public static DeliveryQuest Instance;

    [SerializeField]
    private GameObject _deliveryZone;

    [SerializeField]
    private List<ItemToDelivery> _spawnedItems;
    [SerializeField]
    private List<GameObject> _deliveryItem;

    [SerializeField]
    private List<DeliveryZoneContainer> _pointDictionary;

    [SerializeField]
    private int _reward;

    private bool _isQuestStarted;

    public event System.Action OnQuestEnd;

    private void Start()
    {
        this._isQuestStarted = false;
        Instance = this;
    }

    public void StartQuest(string plrName, string param)
    {
        if (this._isQuestStarted)
            return;

        Transform spawnPos = null;
        for (int i = 0; i < this._pointDictionary.Count; i++)
        {
            if (this._pointDictionary[i].Tag == param)
            {
                spawnPos = this._pointDictionary[i].Points[Random.Range(0,this._pointDictionary[i].Points.Count)];
                break;
            }
        }

        if (spawnPos == null)
            return;

        PlayerUI.Instance.SetQuestText("Find and deliver the target");
        CreateObjects(plrName, spawnPos);

        this._isQuestStarted = true;

        this._deliveryZone.SetActive(true);
    }

    public void EndQuest(ItemToDelivery itm)
    {
        PlayerUI.Instance.CloseQuestText();
        this._isQuestStarted = false;
        DestroyObj(itm.GetPlrName());
        NetworkPlayer.NetworkPlayerInstance.TakeReward(this._reward);

        this._deliveryZone.SetActive(false);

        this.OnQuestEnd?.Invoke();
    }

    private bool CanTakeQuest(string plrName)
    {
        return this._spawnedItems == null;
    }

    private void CreateObjects(string plrName, Transform positionToSpawn)
    {
        if (this._spawnedItems == null)
            this._spawnedItems = new List<ItemToDelivery>();
        
        GameObject item = Instantiate( this._deliveryItem[Random.Range(0, this._deliveryItem.Count)], positionToSpawn.position, positionToSpawn.rotation);

        ItemToDelivery itmSrc = item.GetComponent<ItemToDelivery>();
        itmSrc.ConfigureItem(plrName);

        this._spawnedItems.Add(itmSrc);
    }

    private void DestroyObj(string plrName)
    {
        for (int i = 0; i < this._spawnedItems.Count; i++)
        {
            if (this._spawnedItems[i].GetPlrName() == plrName)
            {
                ItemToDelivery srchSrc = this._spawnedItems[i];
                this._spawnedItems.Remove(srchSrc);
                GameObject.Destroy(srchSrc.gameObject);
            }
        }
    }
}

[System.Serializable]
public class DeliveryZoneContainer
{
    public string Tag;
    public List<Transform> Points;
}