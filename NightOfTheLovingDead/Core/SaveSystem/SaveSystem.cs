using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    public string[] SaveFileNames { get; private set; }

    [SerializeField]
    private PlayerSave _currentLoadedSave;
    private bool _isNewGame;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;

        MouseData.SensetivytyData = PlayerPrefs.GetFloat("Sens");
        VolumeData.Volume = PlayerPrefs.GetFloat("Volume");
        VolumeData.MusicVolume = PlayerPrefs.GetFloat("Music");

        this.SaveFileNames = GetSaveNames();
        EventBus.SubscribeToEvent(EventType.PLAYER_INITIALIZED, RaisSavedData);

        DontDestroyOnLoad(this);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.PLAYER_INITIALIZED, RaisSavedData);
    }

    public void SaveGame()
    {
        PlayerSave playerSave = new PlayerSave(PlayerStats.Instance.BuildDataToSave(),UpgradeSystem.instance.BuildDataToSave());
        DataSerialization.SerializeSave(playerSave);
    }

    public void NewGame()
    {
        this._isNewGame = true;
    }

    public void LoadGame(string saveName)
    {
        this._isNewGame = false;

        PlayerSave playerSave = DataSerialization.DeserializeSave(saveName);
        this._currentLoadedSave = playerSave;
    }


    private string[] GetSaveNames()
    {
        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"\Saves\")))
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"\Saves\"));
        }

        string[] fileNames = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), @"\Saves\"));
        return fileNames;
    }

    private void RaisSavedData(object sender, object param) 
    {
        if (this._currentLoadedSave != null && !this._isNewGame)
        {
            SerializablePlayerData player = null;
            SerializableUpgradeData upgrade = null;

            this._currentLoadedSave.GetData(out player, out upgrade);

            if (player != null)
            {
                EventBus.Dispath(EventType.PLAYERDATA_LOADED, this, player);
            }
            if (upgrade != null)
            {
                EventBus.Dispath(EventType.UPGRADEDATA_LOADED, this, upgrade);
            }
        }
    }
}
