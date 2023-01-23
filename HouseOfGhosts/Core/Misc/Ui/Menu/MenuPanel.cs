using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private MenuPanelType _currentType;

    public void Open()
    {
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public bool IsEqualPanel(string type)
    {
        return type == this._currentType.ToString();
    }
}
