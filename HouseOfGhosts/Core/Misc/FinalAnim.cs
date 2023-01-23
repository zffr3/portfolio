using UnityEngine;
using TMPro;

public class FinalAnim : MonoBehaviour
{
    public static FinalAnim Instance { get; private set; }

    [SerializeField]
    private GameObject _finalCamera;
    [SerializeField]
    private GameObject _finalCanvas;

    [SerializeField]
    private TMP_Text _titleText;

    private void Start()
    {
        Instance = this;
    }

    public void HandleEndOfAnimation()
    {
        Cursor.lockState = CursorLockMode.None;

        this._finalCamera.SetActive(true);
        this._finalCanvas.SetActive(true);

        this.gameObject.SetActive(false);
    }

    public void DisplayTitleText(string text)
    {
        this._titleText.text = text;
    }
}
