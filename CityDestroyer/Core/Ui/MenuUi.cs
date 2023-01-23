using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUi : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _starsText;
    [SerializeField]
    private TMP_Text _moneyText;

    // Start is called before the first frame update
    void Awake()
    {
        EventBus.SubscribeToEvent(EventType.DATA_SHOP_LOADED, ReciveData);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.DATA_SHOP_LOADED, ReciveData);
    }

    private void ReciveData(object sender, object param)
    {
        this._starsText.text = sender.ToString();
        this._moneyText.text = param.ToString();
    }
}
