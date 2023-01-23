using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _altars;

    [SerializeField]
    private List<GameObject> _normalItems;
    [SerializeField]
    private List<GameObject> _curvedItems;

    [SerializeField]
    private int _itemsCount;
    [SerializeField]
    private int _cursedItemsCount;

    [SerializeField]
    private List<Transform> _positionsToSpawn;

    [SerializeField]
    private List<Transform> _selectedPosiions;
    [SerializeField]
    private List<int> _curvedIndexes;
    [SerializeField]
    private int _itemIndex;

    // Start is called before the first frame update
    void Start()
    {
        this._itemIndex = 0;

        EventBus.SubscribeToEvent(EventType.VICTIM_SELECTED, SpawnItems);
        EventBus.SubscribeToEvent(EventType.CURVEDITEM_TAKED, HandleCurvedItemTaked);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.VICTIM_SELECTED, SpawnItems);
        EventBus.UnsubscribeFromEvent(EventType.CURVEDITEM_TAKED, HandleCurvedItemTaked);
    }

    private void SpawnItems(object sender, object param)
    {
        this._altars[Random.Range(0, this._altars.Count)].SetActive(true);

        this._curvedIndexes = param as List<int>;
        this._selectedPosiions = new List<Transform>();

        for (int i = 0; i < this._curvedIndexes.Count; i++)
        {
            this._normalItems.Remove(this._curvedItems[this._curvedIndexes[i]].GetComponent<CurvedItem>().GetNormalItem());

            Transform spawnPos = this._positionsToSpawn[Random.Range(0, this._positionsToSpawn.Count)];
            this._selectedPosiions.Add(spawnPos);
            this._positionsToSpawn.Remove(spawnPos);

            for (int j = 0; j < this._itemsCount; j++)
            {
                if (this._normalItems.Count == 0)
                {
                    return;
                }
                float offset = Random.Range(0.65f, 1.25f);
                GameObject selectedItem = this._normalItems[Random.Range(0, this._normalItems.Count)];
                this._normalItems.Remove(selectedItem);

                Instantiate(selectedItem, this._selectedPosiions[i].position + new Vector3(offset, 0, offset), this._selectedPosiions[i].rotation);
            }
        }

        Instantiate(this._curvedItems[this._curvedIndexes[this._itemIndex]], this._selectedPosiions[this._itemIndex].position, this._selectedPosiions[this._itemIndex].rotation);
    }

    private void HandleCurvedItemTaked(object sender, object param)
    {
        if (this._itemIndex != this._curvedIndexes.Count -1 )
        {
            this._itemIndex++;
            this._cursedItemsCount--;

            EventBus.Dispath(EventType.CURSEDITEMS_COUNT_CHANGED, true, this._cursedItemsCount);

            Instantiate(this._curvedItems[this._curvedIndexes[this._itemIndex]], this._selectedPosiions[this._itemIndex].position, this._selectedPosiions[this._itemIndex].rotation);
        }
        else
        {
            return;
        }
    }
}
