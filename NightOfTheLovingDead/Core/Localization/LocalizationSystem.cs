using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem : MonoBehaviour
{
    public static LocalizationSystem instance;

    [SerializeField]
    private List<LangPackage> packageList;

    [SerializeField]
    private Languages _currentLanguage;
    [SerializeField]
    private LangPackage _currentPackage;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public void ChangeLanguage(int langInt)
    {
        this._currentLanguage = (Languages)langInt;
        this._currentPackage = this.packageList[langInt];

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

    private void SetLanguage(Languages nLang)
    {
        for (int i = 0; i < packageList.Count; i++)
        {
            if (packageList[i].Language == nLang)
            {
                this._currentPackage = packageList[i];
                EventBus.Dispath(EventType.LANGUAGE_CHANGED, this, nLang);
                return;
            }
        }
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
    中文,
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