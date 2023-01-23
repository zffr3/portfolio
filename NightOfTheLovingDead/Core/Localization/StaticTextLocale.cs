using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StaticTextLocale : MonoBehaviour
{
    [SerializeField]
    private Text _textSrc;

    [SerializeField]
    private LanguageKey _key;

    // Start is called before the first frame update
    void Awake()
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
        this._textSrc.text = LocalizationSystem.instance.GetWord(this._key);
    }
}
