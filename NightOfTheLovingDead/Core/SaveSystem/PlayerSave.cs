using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSave 
{
    private SerializablePlayerData _playerStats;
    private SerializableUpgradeData _upgradeData;

    public PlayerSave(SerializablePlayerData pStat, SerializableUpgradeData uData)
    {
        this._playerStats = pStat;
        this._upgradeData = uData;
    }

    public void GetData(out SerializablePlayerData pStat, out SerializableUpgradeData uData)
    {
        pStat = this._playerStats;
        uData = this._upgradeData;
    }
}
