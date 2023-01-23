using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTwitchCommand : ICommand
{
    public string CommandName { get; set; }

    public void Execute(object param)
    {
        string message = param as string;

        int splitPoint = message.IndexOf("!", 1);
        string chatName = message.Substring(0, splitPoint);

        chatName = chatName.Substring(1);

        splitPoint = message.IndexOf(":", 1);
        message = message.Substring(splitPoint + 1);

        Debug.Log(message);
    }

}
