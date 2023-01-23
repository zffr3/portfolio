using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchListener : MonoBehaviour
{
    [SerializeField]
    private CommandProcessor _processor;

    // Start is called before the first frame update
    void Start()
    {
        this._processor = this.GetComponent<CommandProcessor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TwitchConnect.IsConnected && this._processor != null)
        {
            string message = TwitchConnect.ReadChat();
            Debug.Log(message);
            if (!string.IsNullOrEmpty(message))
            {
                Debug.Log(message);
                this._processor.FindeAndExecute(message, null);
            }
        }
    }
}
