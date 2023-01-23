using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NetworkMenuText : MonoBehaviour
{
    public static NetworkMenuText Instance { get; private set; }

    [SerializeField]
    private TMP_InputField _roomNameInputField;

    [SerializeField]
    private TMP_Text _errorText;
    [SerializeField]
    private TMP_Text _roomNameText;

    private void Awake()
    {
        Instance = this;
    }

    public string GetRoomName()
    {
        return this._roomNameInputField.text;
    }

    public void SetErrorText(string errorData)
    {
        this._errorText.text = errorData;
    }

    public void SetRoomNameText(string roomName)
    {
        this._roomNameText.text = roomName;
    }

    public string GetPlayerNick()
    {
        return $"Player{Random.Range(0, 9999).ToString("0000")}";
    }
}
