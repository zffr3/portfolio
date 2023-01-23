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

    void Awake()
    {
        EventBus.SubscribeToEvent(EventType.LANGUAGE_CHANGED, ChangeText);
        EventBus.SubscribeToEvent(EventType.UI_PANEL_LOADED, ChangeText);

        ChangeText(this, this._key);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.LANGUAGE_CHANGED, ChangeText);
        EventBus.UnsubscribeFromEvent(EventType.UI_PANEL_LOADED, ChangeText);
    }

    private void ChangeText(object sender, object param)
    {
        this._textSrc.text = LocalizationSystem.instance.GetWord(this._key);
    }

}
