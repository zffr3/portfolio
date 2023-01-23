using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimController : MonoBehaviour
{
    [SerializeField]
    private List<VictimData> _victims;

    [SerializeField]
    private VictimData _currentVictim;

    private void Start()
    {
        SelectVictim();
    }

    private void SelectVictim()
    {
        this._currentVictim = this._victims[Random.Range(0, this._victims.Count)];
        EventBus.Dispath(EventType.VICTIM_SELECTED, this, GetItemIndexes());
    }

    public bool CheckVictim(string selectedVictim)
    {
        if (this._currentVictim.SystemName == selectedVictim)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private List<int> GetItemIndexes()
    {
        return this._currentVictim.VictimCurvedItems;
    }
}
