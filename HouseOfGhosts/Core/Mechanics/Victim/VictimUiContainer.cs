using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictimUiContainer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _nameTxt;
    [SerializeField]
    private TMP_Text _bioTxt;

    [SerializeField]
    private VictimData _viktimProfile;

    private void Start()
    {
        if (this._viktimProfile != null)
        {
            StringData viktimDisplayData = this._viktimProfile.GetDisplayData(Language.EN);

            if (viktimDisplayData != null)
            {
                this._nameTxt.text = viktimDisplayData.Name;
                this._bioTxt.text = viktimDisplayData.Description + viktimDisplayData.DeathReason;
            }
        }
    }

    public void SelectVictim()
    {
        if (this._viktimProfile != null)
        {
            EndGameController.Instance.CheckVictimAndEndGame(this._viktimProfile.SystemName);
        }
    }
}
