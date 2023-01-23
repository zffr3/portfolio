using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingInventory : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _inventory;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.SubscribeToEvent(EventType.TRAININGSTATE_CHANGED, ActivateItem);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.TRAININGSTATE_CHANGED, ActivateItem);
    }

    private void ActivateItem(object sender, object param)
    {
        int itemIndex = (int)((EventType)param);
        if (itemIndex >= 0 && itemIndex < this._inventory.Count)
        {
            for (int i = 0; i < this._inventory.Count; i++)
            {
                this._inventory[i].SetActive(false);
            }
            this._inventory[itemIndex].SetActive(true);
        }

    }
}
