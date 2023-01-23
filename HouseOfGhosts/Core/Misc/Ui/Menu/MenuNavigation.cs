using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    public static MenuNavigation Instance { get; private set; }

    [SerializeField]
    private MenuPanel[] _panels;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(MenuPanelType type)
    {
        OpenMenu(type.ToString());
    }

    public void OpenMenu(string type)
    {
        for (int i = 0; i < this._panels.Length; i++)
        {
            if (this._panels[i].IsEqualPanel(type))
            {
                OpenMenuPanel(this._panels[i]);
            }
            else if (this._panels[i].gameObject.activeSelf)
            {
                CloseMenu(this._panels[i]);
            }
        }
    }

    public void OpenMenu(MenuPanel menu)
    {
        for (int i = 0; i < this._panels.Length; i++)
        {
            if (this._panels[i].gameObject.activeSelf)
            {
                CloseMenu(this._panels[i]);
            }
        }

        menu.Open();
    }

    private void OpenMenuPanel(MenuPanel panel)
    {
        panel.Open();
    }

    private void CloseMenu(MenuPanel panel)
    {
        panel.Close();
    }
}

[System.Serializable]
public enum MenuPanelType
{
    Loading,
    NetworkTitle,
    CreateRoom,
    Error,
    Room, 
    FindRoom
}
