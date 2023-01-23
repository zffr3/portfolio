using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizationSystem : MonoBehaviour
{
    public static LocalizationSystem instance;

    [SerializeField]
    private List<LangPackage> packageList;

    [SerializeField]
    private Languages _currentLanguage;
    [SerializeField]
    private LangPackage _currentPackage;

    private int _savedIndex;

    private void Awake()
    {
        instance = this;

        this._savedIndex = PlayerPrefs.GetInt("Lang");
        ChangeLanguage(this._savedIndex);
    }

    public void ChangeLanguage(int langInt)
    {
        this._currentPackage = this.packageList[langInt];
        this._currentLanguage = this._currentPackage.Language;

        PlayerPrefs.SetInt("Lang", langInt);

        SetLanguage(this._currentLanguage);
    }

    public string GetWord(LanguageKey key)
    {
        return this._currentPackage.GetWordByKey(key);
    }

    public List<string> GetAvailableLanguages()
    {
        List<string> availableLanguages = new List<string>();

        for (int i = 0; i < packageList.Count; i++)
        {
            availableLanguages.Add(packageList[i].Language.ToString());
        }

        return availableLanguages;
    }

    public TMP_FontAsset GetCurrentFont()
    {
        return this._currentPackage.GetFont();
    }

    public bool IsCustomFontNeeded()
    {
        return this._currentPackage.UseCustomFont();
    }

    private void SetLanguage(Languages nLang)
    {
        EventBus.Dispath(EventType.LANGUAGE_CHANGED, this, nLang);
    }
}
public enum Languages
{
    English,
    Русский,
    Italiano,
    Español,
    Ceský,
    Deutsch,
    Português,
    Türk,
    Việt_nam,
    Polski,
    Dansk,
    Chinese,
    한국어,
    Norsk,
    Suomalainen, 
    Svensk,
    Français,
    Български,
    Magyar, 
    Ελληνική, 
    Nederlands,
    Romanian,
    ไทย,
    Український,
    日本語,
    العربية
}