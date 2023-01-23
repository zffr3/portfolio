using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchPersonalData
{
    private string _userName;
    private string _password;
    private string _channelName;

    public string UserName 
    {
        get 
        {
            return this._userName;
        } 
        set
        {
            this._userName = value;
        }
    }

    public string Password
    {
        get 
        {
            return this._password;
        } 
        set
        {
            this._password = value;
        }
    }

    public string ChannelName
    {
        get
        {
            return this._channelName; 
        }
        set
        {
            this._channelName = value;
        }
    }
}
