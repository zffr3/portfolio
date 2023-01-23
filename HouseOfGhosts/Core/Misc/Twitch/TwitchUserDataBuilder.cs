using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwitchUserDataBuilder : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _userNameInp;
    [SerializeField]
    private TMP_InputField _chanelInp;
    [SerializeField]
    private TMP_InputField _chatApiKeyInp;

   public void BuildDataAndConnect()
    {
        TwitchPersonalData data = new TwitchPersonalData() 
        {
            UserName = this._userNameInp.text,
            ChannelName = this._chanelInp.text,
            Password = this._chatApiKeyInp.text,
        };
        Debug.Log(data.UserName);
        Debug.Log(data.ChannelName);
        TwitchConnect.SetUserDataAndConnect(data);
    }
}
