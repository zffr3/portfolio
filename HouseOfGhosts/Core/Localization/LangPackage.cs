using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New langPack", menuName = "Language", order = 1)]
public class LangPackage : ScriptableObject
{
    [SerializeField]
    private Languages _language;

    public Languages Language { get { return this._language; } }

    [SerializeField]
    private List<LanguageRow> _wordDataBase;

    [SerializeField]
    private bool _useUnusualFont;
    [SerializeField]
    private TMPro.TMP_FontAsset _fontAsset;

    public string GetWordByKey(LanguageKey key)
    {
        for (int i = 0; i < this._wordDataBase.Count; i++)
        {
            if (this._wordDataBase[i].Key == key)
            {
                return this._wordDataBase[i].Word;
            }
        }

        return null;
    }

    public TMPro.TMP_FontAsset GetFont()
    {
        return this._fontAsset;
    }

    public bool UseCustomFont()
    {
        return this._useUnusualFont;
    }

    [System.Serializable]
    public class LanguageRow 
    {

        [SerializeField]
        private LanguageKey _key;
        public LanguageKey Key { get { return this._key; } }

        [SerializeField]
        private string _word;
        public string Word { get { return this._word; } }
    }
}
public enum LanguageKey 
{
    Play,
    Settings,
    Quit,
    Back,
    Game,
    Quality,
    Other,
    Sensitivity,
    MusicVolume,
    SoundVolume,
    QualityLevel,
    Resolution,
    Windowed,
    Apply,
    PlayNewGame,
    StartTraining,
    Yes,
    No,
    Language,
    TrainingRound1_Text1,
    TrainingRound1_Text2,
    TrainingRound2_Text1,
    TrainingRound2_Text2,
    TrainingRound3_Text1,
    TrainingRound3_Text2,
    TrainingRound4_Text1,
    TrainingRound4_Text2,
    TrainingRound5_Text1,
    TrainingRound5_Text2,
    TrainingRound6_Text1,
    TrainingRound6_Text2,
    TrainingRound7_Text1,
    TrainingRound7_Text2,
    Interact,
    VHS,
    Bracelet,
    Herbs,
    Lighter,
    Notepad,
    Keys,
    Book,
    Feedback
}
