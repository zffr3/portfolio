using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _inventory;

    [SerializeField]
    private int _currentItem;

    [SerializeField]
    private Transform _parent;

    // Start is called before the first frame update
    void Start()
    {
        this._currentItem = 0;

        EventBus.SubscribeToEvent(EventType.LEFTITEM_PRESSED, NextItem);
        EventBus.SubscribeToEvent(EventType.RIGHTITEM_PRESSED, PrevItem);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.LEFTITEM_PRESSED, NextItem);
        EventBus.UnsubscribeFromEvent(EventType.RIGHTITEM_PRESSED, PrevItem);
    }

    public void NextItem(object sender, object param)
    {
        if (this._inventory[this._currentItem] != null)
        {
            this._inventory[this._currentItem].SetActive(false);
        }

        if (this._currentItem + 1 < this._inventory.Count)
        {
            if (this._inventory[this._currentItem + 1] == null)
            {
                this._currentItem = 0;
            }
            else
            {
                this._currentItem++;
            }
        }
        else
        {
            this._currentItem = 0;
        }
        this._inventory[this._currentItem].SetActive(true);
    }

    public void PrevItem(object sender, object param)
    {
        if (this._inventory[this._currentItem] != null)
        {
            this._inventory[this._currentItem].SetActive(false);
        }

        if (this._currentItem - 1 >= 0)
        {
            this._currentItem--;
        }
        else
        {
            for (int i = this._inventory.Count-1; i > 0; i--)
            {
                if (this._inventory[i] != null && this._inventory[i] != this._inventory[this._currentItem])
                {
                    this._currentItem = i;
                    break;
                }
            }
        }
        this._inventory[this._currentItem].SetActive(true);
    }

    public void AddItem(GameObject item)
    {
        int newItemIndex = 0;
        for (int i = 0; i < this._inventory.Count; i++)
        {
            if (this._inventory[i] == null)
            {
                newItemIndex = i;
                break;
            }
        }

        if (newItemIndex != 0)
        {
            item.SetActive(false);
            this._inventory[newItemIndex] = item;

            item.transform.parent = this._parent;
            item.transform.position = this._parent.transform.position;

            Rigidbody itemBody = item.GetComponent<Rigidbody>();

            item.tag = "Untagged";

            if (itemBody != null)
            {
                itemBody.constraints = RigidbodyConstraints.FreezePosition;
            }
        }
    }
}
