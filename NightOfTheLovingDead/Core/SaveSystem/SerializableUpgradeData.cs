using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableUpgradeData 
{
    public float WeaponUpgradePercent { get; private set; }
    public float HealthUpgradePercent { get; private set; }
    public float PowerUpgradePercent { get; private set; }

    public SerializableUpgradeData(float wUpgrade, float hUpgrade, float pUpgrade)
    {
        this.WeaponUpgradePercent = wUpgrade;
        this.HealthUpgradePercent = hUpgrade;
        this.PowerUpgradePercent = pUpgrade;
    }
}
