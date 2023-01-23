using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Victim", menuName = "Victim Data", order = 51)]
public class VictimData : ScriptableObject
{
    public string SystemName;

    public List<StringData> DisplayData;

    public List<int> VictimCurvedItems;

    public StringData GetDisplayData(Language targetLang)
    {
        foreach (StringData item in this.DisplayData)
        {
            if (item.CurrentLanguage == targetLang)
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]
public class StringData
{
    [SerializeField]
    public Language CurrentLanguage;

    [SerializeField]
    public string Name;

    [SerializeField]
    public string Description;

    [SerializeField]
    public string DeathReason;
}
public enum Language
{
    EN,
    RU
}