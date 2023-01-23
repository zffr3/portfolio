using System.Collections;
using System.Collections.Generic;

public interface ICommand 
{
    public string CommandName { get; set; }

    void Execute(object param);
}
