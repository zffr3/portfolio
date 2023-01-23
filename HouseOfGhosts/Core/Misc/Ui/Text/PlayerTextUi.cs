using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class PlayerTextUi : MonoBehaviour
{
    [SerializeField]
    private List<TextContainer> _staticTextContainers;
    [SerializeField]
    private List<TextContainer> _dynamicTextContainers;

    private void Start()
    {
        if (this._staticTextContainers == null)
        {
            this._staticTextContainers = new List<TextContainer>();
        }

        if (this._dynamicTextContainers == null)
        {
            this._dynamicTextContainers = new List<TextContainer>();
        }
    }

    public void SetStaticText(UiTextKey key, string text)
    {
        if (this._staticTextContainers.Count == 0)
        {
            return;
        }

        foreach (TextContainer item in this._staticTextContainers)
        {
            if (item.Key == key)
            {
                item.SetText(text);
                return;
            }
        }
    }

    public async void SetDynamicText(UiTextKey key, string text, int delay)
    {
        if (this._dynamicTextContainers.Count == 0)
        {
            return;
        }

        foreach (TextContainer item in this._dynamicTextContainers)
        {
            if (item.Key == key)
            {
                item.ChangeActivationState(true);
                item.SetText(text);

                await Task.Delay(delay);

                item.ChangeActivationState(false);

                return;
            }
        }

    }

    [System.Serializable]
    public class TextContainer
    {
        [SerializeField]
        private UiTextKey _key;

        public UiTextKey Key { get { return _key; } }

        [SerializeField]
        private TMP_Text _textSrc;

        public void SetText(string newText)
        {
            if (this._textSrc != null)
            {
                this._textSrc.text = newText;
            }
        }

        public void ChangeActivationState(bool newState)
        {
            if (this._textSrc != null)
            {
                this._textSrc.gameObject.SetActive(newState);
            }
        }
    }
}

public enum UiTextKey
{
    Dynamic_Notification
}