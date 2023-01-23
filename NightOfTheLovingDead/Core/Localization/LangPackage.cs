using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New langPack", menuName = "Language", order = 1)]
public class LangPackage : ScriptableObject
{
    public Languages Language;

    [SerializeField]
    public List<LanguageRow> WordDataBase;

    public string GetWordByKey(LanguageKey key)
    {
        for (int i = 0; i < this.WordDataBase.Count; i++)
            if (this.WordDataBase[i].Key == key)
                return this.WordDataBase[i].Word;
        return null;
    }

    [System.Serializable]
    public class LanguageRow 
    {
        [SerializeField]
        public LanguageKey Key;
        [SerializeField]
        public string Word;
    }
}
public enum LanguageKey 
{
    Play,
    StartGame,
    Settings,
    Quit,
    Back,
    Leave,
    Quality,
    Game,
    Other,
    FpsCounter,
    Language,
    Qlevel,
    PpLevel,
    Resolution,
    Windowed,
    MouseSens,
    MusicVolume,
    SoundVolume
}
