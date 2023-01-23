using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionSettings : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _resolutionDropdown;
    [SerializeField]
    private int _minHeight;
    [SerializeField]
    private Button _applyButton;
    [SerializeField]
    private Toggle _fullScreenToggle;

    private Resolution[] _resolutionList;
    private int _resolutionId;

    // Start is called before the first frame update
    void Start()
    {
        this._resolutionList = Screen.resolutions;
        ReBuildResolutionsList();
        SetDropdownMenu();
        this._fullScreenToggle.isOn = Screen.fullScreen;
        this._applyButton.interactable = false;
    }

    private string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }

    private void SetDropdownMenu()
    {
        this._resolutionDropdown.options = new List<TMP_Dropdown.OptionData>();

        for (int i = 0; i < this._resolutionList.Length; i++)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = ResToString(this._resolutionList[i]);
            this._resolutionDropdown.options.Add(option);
            if (this._resolutionList[i].height == Screen.height && this._resolutionList[i].width == Screen.width)
            {
               this. _resolutionId = i;
            }
        }

        this._resolutionDropdown.value = this._resolutionId;
        this._resolutionDropdown.onValueChanged.AddListener(delegate { SetID(); });
        this._fullScreenToggle.onValueChanged.AddListener(delegate { SetID(); });
        this._applyButton.onClick.AddListener(() => { ApplyResolution(); });
    }

    private void SetID()
    {
        this._applyButton.interactable = true;
        this._resolutionId = this._resolutionDropdown.value;
    }

    private void ApplyResolution()
    {
        this._applyButton.interactable = false;
        Screen.SetResolution(this._resolutionList[this._resolutionId].width, this._resolutionList[this._resolutionId].height, this._fullScreenToggle.isOn);
    }

    void ReBuildResolutionsList()
    {
        int x = 0;
        foreach (Resolution element in this._resolutionList)
        {
            if (element.height >= this._minHeight) x++;
        }
        Resolution[] pureArray = new Resolution[x];
        x = 0;
        foreach (Resolution element in this._resolutionList)
        {
            if (element.height >= this._minHeight)
            {
                pureArray[x] = element;
                x++;
            }
        }
        this._resolutionList = new Resolution[pureArray.Length];
        for (int i = 0; i < this._resolutionList.Length; i++)
        {
            this._resolutionList[i] = pureArray[i];
        }
    }
}
