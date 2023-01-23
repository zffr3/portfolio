using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedTrigger : MonoBehaviour
{
    [SerializeField]
    private Altar _altarSrc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CurvedItem>())
        {
            EventBus.Dispath(EventType.CURVEDITEM_TAKED, this, this);
            Destroy(other.gameObject);
            this._altarSrc.ActivateAltar(true);

            EventBus.Dispath(EventType.ALTAR_ACTIVATED, this, this);
        }
        if (other.GetComponent<NormalItem>())
        {
            EventBus.Dispath(EventType.NORMALITEM_TAKED, this, this);
            Destroy(other.gameObject);
            this._altarSrc.ActivateAltar(false);
            this._altarSrc.TakeWrongItem();

            EventBus.Dispath(EventType.ALTAR_ACTIVATED, this, this);
        }
    }
}
