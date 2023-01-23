using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class NoldQualitySettings : MonoBehaviour
{
    [SerializeField]
    private int _savedQualityLvl;

    [SerializeField]
    private TMP_Dropdown _qualityDropdown;
    [SerializeField]
    private TMP_Dropdown _ppVolQuality;
    [SerializeField]
    private TMP_Dropdown _resolutionDropDown;
    [SerializeField]
    private Toggle _windowedToggle;

    [SerializeField]
    private List<ResolutionData> _resolutions;

    [SerializeField]
    private PostProcessVolume _ppV;
    [SerializeField]
    private List<PostProcessProfile> _profiles;

    [SerializeField]
    private bool _isWindowed;

    [SerializeField]
    private int _currentResolution;

    [SerializeField]
    private bool _usePp;

    [SerializeField]
    private int _currentPpVolume;

    private void Start()
    {
        int qTemp = QualitySettings.GetQualityLevel();
        this._savedQualityLvl = PlayerPrefs.GetInt("Quality");
        this._qualityDropdown.value = this._savedQualityLvl;

        if (qTemp != this._savedQualityLvl)
        {
            SetQualityLevel(this._savedQualityLvl);
        }

        this._resolutionDropDown.value = GetCurrentResolution();
        this._windowedToggle.isOn = !Screen.fullScreen;

        if (this._usePp)
        {
            this._currentPpVolume = PlayerPrefs.GetInt("PpVolume");
            this._ppVolQuality.value = this._currentPpVolume;
            if (this._ppV != null)
            {
                if (this._ppV.profile != this._profiles[this._currentPpVolume])
                {
                    SetPpVolume(this._currentPpVolume);
                }
            }
        }
    }

    private void SetAndSaveQuality(int qLvl)
    {
        if (this._savedQualityLvl == qLvl)
        {
            return;
        }

        PlayerPrefs.SetFloat("Quality", qLvl);
        SetQualityLevel(qLvl);
    }

    private void SetQualityLevel(int qLvl)
    {
        if (this._savedQualityLvl == qLvl)
        {
            return;
        }

        QualitySettings.SetQualityLevel(qLvl,true);
    }

    public void SetWindowMode()
    {
        if (Screen.fullScreen == this._windowedToggle.isOn)
        {
            Screen.fullScreen = !this._windowedToggle.isOn;
        }
    }

    public void SetResolution()
    {
        if (this._resolutions[this._resolutionDropDown.value].Heigh != Screen.currentResolution.height || this._resolutions[this._resolutionDropDown.value].Width != Screen.currentResolution.width)
        {
            Screen.SetResolution(this._resolutions[this._resolutionDropDown.value].Width, this._resolutions[this._resolutionDropDown.value].Heigh, this._windowedToggle.isOn);
        }
    }

    private void SetAndSavePpVolume(int pp)
    {
        PlayerPrefs.SetInt("PpVolume",pp);
        SetPpVolume(pp);
    }

    private void SetPpVolume(int pp)
    {
        this._ppV.profile = this._profiles[pp];
    }

    public void OnQualityChanged()
    {
        SetAndSaveQuality(this._qualityDropdown.value);
    }

    public void OnPpVolumeChanged()
    {
        SetAndSavePpVolume(this._ppVolQuality.value);
    }

    private int GetCurrentResolution()
    {
        for (int i = 0; i < this._resolutions.Count; i++)
        {
            if (Screen.currentResolution.height == this._resolutions[i].Heigh && Screen.currentResolution.width == this._resolutions[i].Width)
            {
                return i;
            }
        }

        return 0;
    }


    [System.Serializable]
    public struct ResolutionData
    {
        public int Width;
        public int Heigh;
    }
}
