using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class LocalizationUi : MonoBehaviour
{
    [SerializeField]
    private Dropdown _langListDd;

    private void Start()
    {
        List<string> list = LocalizationSystem.instance.GetAvailableLanguages();
        List<OptionData> options = new List<OptionData>();


        foreach (string lang in list)
        {
            options.Add(new OptionData() { text = lang });
        }

        this._langListDd.options.Clear();
        this._langListDd.options.AddRange(options);
    }

    public void ChangeLanguage(int ind)
    {
        LocalizationSystem.instance.ChangeLanguage(this._langListDd.value);
    }
}
