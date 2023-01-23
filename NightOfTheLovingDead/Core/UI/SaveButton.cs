using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    [SerializeField]
    private SaveUi _src;

    [SerializeField]
    private Text _saveNameView;

    [SerializeField]
    private string _saveName;

    public void ConfigureBtn(string sName, SaveUi src)
    {
        if (this._saveNameView == null)
        {
            this._saveNameView = this.GetComponentInChildren<Text>();
        }

        this._saveName = sName;
        this._saveNameView.text = this._saveName;
        this._src = src;
    }

    public void ClickLoadSave()
    {
        this._src.LoadSave(this._saveName);
    }

    public void ClickSaveGame()
    {
        SaveSystem.instance.SaveGame();
    }
}
