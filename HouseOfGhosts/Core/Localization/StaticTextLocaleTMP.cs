using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class StaticTextLocaleTMP : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textSrc;

    [SerializeField]
    private LanguageKey _key;

    void Start()
    {
        EventBus.SubscribeToEvent(EventType.LANGUAGE_CHANGED, ChangeText);
        ChangeText(this, this._key);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.LANGUAGE_CHANGED, ChangeText);
    }

    private void ChangeText(object sender, object param)
    {
        if (LocalizationSystem.instance.IsCustomFontNeeded())
        {
            this._textSrc.font = LocalizationSystem.instance.GetCurrentFont();
        }

        this._textSrc.text = LocalizationSystem.instance.GetWord(this._key);
    }

}
