using System.Collections.Generic;
using UnityEngine;

public class SaveUi : MonoBehaviour
{
    public static SaveUi Instance;

    [SerializeField]
    private List<SaveButton> _saveButtons;

    private void Start()
    {
        Instance = this;
        LoadSaveBtns(SaveSystem.instance.SaveFileNames);
    }

    private void OnDisable()
    {
        DisableButtons();
    }

    private void OnDestroy()
    {
        DisableButtons();
    }

    private void LoadSaveBtns(string[] saveList)
    {
        if (saveList == null || saveList.Length == 0)
        {
            return;
        }

        for (int i = 0; i < saveList.Length; i++)
        {
            if (i >= this._saveButtons.Count)
            {
                return;
            }

            this._saveButtons[i].gameObject.SetActive(true);
            this._saveButtons[i].ConfigureBtn(saveList[i],this);
        }
    }

    public void LoadSave(string saveName)
    {
        Launcher.Instance.LoadWithSave(saveName);
    }

    private void DisableButtons()
    {
        for (int i = 0; i < this._saveButtons.Count; i++)
        {
            this._saveButtons[i].gameObject.SetActive(false);
        }
    }
}
