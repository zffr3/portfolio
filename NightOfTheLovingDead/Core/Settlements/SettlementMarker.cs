using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementMarker : MonoBehaviour
{
    [SerializeField]
    private Settlement _referenceSettlement;

    public void InteractWitgSettlement(QuestNpcType type)
    {
        switch (type)
        {
            case QuestNpcType.ClearZone:
                ClearSettlement();
                break;
            case QuestNpcType.Delivery:
                AddSettlementSupplies();
                break;
            default:
                break;
        }
    }

    public void ClearSettlement()
    {
        this._referenceSettlement.ActivateSettlement();
    }

    public void AddSettlementSupplies()
    {
        this._referenceSettlement.AddSuplies();
    }
}
