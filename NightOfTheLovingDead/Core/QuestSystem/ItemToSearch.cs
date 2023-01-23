using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToSearch : MonoBehaviour
{

    [SerializeField]
    private string _playerWhoStartedQuest;

    public void ConfigureItem(string plrName)
    {
        this._playerWhoStartedQuest = plrName;
    }

    public string GetPlrName()
    {
        return this._playerWhoStartedQuest;
    }

    private void OnTriggerEnter(Collider other)
    {
        NetworkCharacter charSrc = other.GetComponent<NetworkCharacter>();
        if (charSrc != null && charSrc.CheckPlayerName(this._playerWhoStartedQuest))
        {
            FindQuest.Instance.EndQuest(this);
        }
    }
}
