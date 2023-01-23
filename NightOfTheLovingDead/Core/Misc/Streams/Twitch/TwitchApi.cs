using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;

public class TwitchApi : MonoBehaviour
{
    private TcpClient _twitchClient;
    private StreamReader _reader;
    private StreamWriter _writer;

    private const string  TWITCH_API_URL = "irc.chat.twitch.tv";
    private const int TWITCH_API_PORT = 6667;

    [SerializeField]
    private string _userName;
    [SerializeField]
    private string _password;
    [SerializeField]
    private string _chanelName;

    // Start is called before the first frame update
    void Start()
    {
        ConnectToTwitchApi();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this._twitchClient.Connected)
            ConnectToTwitchApi();

        ReadChat();
    }

    private void ConnectToTwitchApi()
    {
        this._twitchClient = new TcpClient(TWITCH_API_URL, TWITCH_API_PORT);
        this._reader = new StreamReader(this._twitchClient.GetStream());
        this._writer = new StreamWriter(this._twitchClient.GetStream());

        this._writer.WriteLine($"PASS {this._password}");
        this._writer.WriteLine($"NICK {this._userName}");
        this._writer.WriteLine($"USER {this._userName} 8 * :{this._userName}");
        this._writer.WriteLine($"JOIN #{this._chanelName}");
        this._writer.Flush();
    }

    private void ReadChat()
    {
        if (this._twitchClient.Available > 0)
        {
            string message = this._reader.ReadLine();
            print(message);
        }
    }
}
