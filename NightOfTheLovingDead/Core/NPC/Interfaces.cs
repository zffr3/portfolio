using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    void Move();
    void StopMoving();
    void ContinueMoving();
}
public interface ITrade
{
    void Trade();
}
public interface IQuest 
{
    void StartQuest(string pName);
    void StartQuestWithParams(string pName, string param);
}

public interface ISpeak
{
    void SpeakWithPlayer();
}

public interface INpc
{
    void Configure(Transform target, Settlement src, System.Action<GameObject> dieHandler);
    void Die();
}