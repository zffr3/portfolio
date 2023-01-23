using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptQuest : MonoBehaviour
{
    public static ScriptQuest Instance;

    public event System.Action<string> OnQuestEnded;

    [SerializeField]
    private GameObject[] _sriptQuests;


    [SerializeField]
    private int _scriptQuestPointer;

    private void Start()
    {
        Instance = this;
        this._scriptQuestPointer = 0;
    }

    public void TakeQuest(string plrName)
    {
    }

    public void CheckNpcNameAndTakeQuest(string plrName, string npcName)
    {

    }

    public void EndQuest()
    {
        if (this._sriptQuests.Length == this._scriptQuestPointer)
            this._scriptQuestPointer = 0;
        else
            this._scriptQuestPointer++;
    }
}