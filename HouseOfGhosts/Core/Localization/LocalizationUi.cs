using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizationUi : MonoBehaviour
{
    [SerializeField]
    private LocalizationSystem _localizationSrc;

    [SerializeField]
    private TMP_Dropdown _langListDd;

    private void Start()
    {
        List<string> langList = this._localizationSrc.GetAvailableLanguages();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();


        foreach (string lang in langList)
        {
            options.Add(new TMP_Dropdown.OptionData() { text = lang });
        }

        this._langListDd.options.Clear();
        this._langListDd.options.AddRange(options);
    }

    public void ChangeLanguage()
    {
        LocalizationSystem.instance.ChangeLanguage(this._langListDd.value);
    }
}
