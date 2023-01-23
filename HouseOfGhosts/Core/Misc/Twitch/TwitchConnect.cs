using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;

public static class TwitchConnect
{
    private const string CONNECTION_URL = "irc.chat.twitch.tv";
    private const int CONNECTION_PORT = 6667;

    private static TcpClient _twitchClient;
    private static StreamReader _reader;
    private static StreamWriter _writer;

    private static TwitchPersonalData _userData;

    public static bool IsConnected { get; private set; }

    public static void SetUserDataAndConnect(TwitchPersonalData data)
    {
        _userData = data;
        Connect();
    }

    public static void Connect()
    {
        if (_userData == null)
        {
            return;
        }

        _twitchClient = new TcpClient(CONNECTION_URL, CONNECTION_PORT);

        _reader = new StreamReader(_twitchClient.GetStream());
        _writer = new StreamWriter(_twitchClient.GetStream());

        _writer.WriteLine("PASS " +_userData.Password);
        _writer.WriteLine("NICK " + _userData.UserName);
        //_writer.WriteLine("USER " + _userData.UserName + " 8 * :" + _userData.UserName);
        _writer.WriteLine("JOIN #" + _userData.ChannelName);

        _writer.Flush();  
        
        IsConnected = _twitchClient.Connected;
    }

    public static string ReadChat()
    {
        if (_twitchClient.Available > 0)
        {
            string message = _reader.ReadLine();

            if (message.Contains("PRIVMSG"))
            {
                return message;
            }
        }

        return _reader.ReadLine();
    }

    public static void KeepConnectionAlive()
    {
        if (_twitchClient.Connected)
        {
            _writer.WriteLine("ping");
            _writer.Flush();   
        }
    }
}
