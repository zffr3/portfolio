using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandProcessor : MonoBehaviour
{
    public static CommandProcessor Instance { get; private set; }

    private List<ICommand> _commands;

    private ICommand _defaultCommand;

    // Start is called before the first frame update
    void Start()
    {
        this._commands = new List<ICommand>();

        Instance = this;
    }

    public bool FindeAndExecute(string name, object param)
    {
        if (this._commands.Count != 0)
        {
            for (int i = 0; i < this._commands.Count; i++)
            {
                if (this._commands[i].CommandName == name)
                {
                    this._commands[i].Execute(param);
                    return true;
                }
            }

            if (this._defaultCommand != null)
            {
                this._defaultCommand.Execute(name);
            }
        }
        return false;
    }
}
