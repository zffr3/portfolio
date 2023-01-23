using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingUi : MonoBehaviour
{
    public static TrainingUi Instance { get; private set; }

    [SerializeField]
    private TMP_Text _textSource;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void DisplayMessage(string text)
    {
        this._textSource.text = text;
    }

    public void DisplayMessageAndClear(string text)
    {
        this._textSource.text = text;
        StartCoroutine(ClearText());
    }

    private IEnumerator ClearText()
    {
        yield return new WaitForSecondsRealtime(5f);
        this._textSource.text = string.Empty;
    }
}
