using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QualityLevelSettings : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _qualityDropdown;

    // Start is called before the first frame update
    void Start()
    {
        List<TMP_Dropdown.OptionData> qualityData = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            qualityData.Add(new TMP_Dropdown.OptionData(QualitySettings.names[i]));
        }

        this._qualityDropdown.options = qualityData;
    }

    public void ChangeQualityLevel()
    {
        QualitySettings.SetQualityLevel(this._qualityDropdown.value);
    }
}
