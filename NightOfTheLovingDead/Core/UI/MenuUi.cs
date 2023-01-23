using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUi : MonoBehaviour
{

    public static MenuUi Instance;

    [SerializeField]
    private List<GameObject> _uiPanels;
    [SerializeField]
    private TMP_Text _errorTxt;

    public List<GameObject> UiPanels
    {
        get
        {
            return this._uiPanels;
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void DiactivateAllPanels()
    {
        foreach (GameObject item in this.UiPanels)
            item.SetActive(false); ;
    }

    public void ActivatePanel(string name)
    {

        foreach (GameObject item in this.UiPanels)
        {
            if (item.name == name)
            {
                ActivatePanel(item);
                break;
            }
        }
    }

    public void ActivatePanel(GameObject panel)
    {
        DiactivateAllPanels();
        panel.SetActive(true);
    }

    public void ShowError(string errorMsg)
    {
        ActivatePanel("ErrorPanel");
        this._errorTxt.text = errorMsg;
    }

    public void SetUiPanelList(List<GameObject> panels)
    {
        this._uiPanels = panels;
    }

    public void SetVisualErrorTextSource(TMP_Text txt)
    {
        this._errorTxt = txt;
    }
}
