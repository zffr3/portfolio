using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private int _chapter;
    [SerializeField]
    private int _currentLevel;

    [SerializeField]
    private bool _dropLevel;

    // Start is called before the first frame update
    void Start()
    {
        this._chapter = PlayerPrefs.GetInt("chapter");
        this._currentLevel = PlayerPrefs.GetInt("level");

        EventBus.SubscribeToEvent(EventType.LEVEL_CLOSED, HandleLevelClosed);
        EventBus.Dispath(EventType.LEVEL_IND_LOADED, this, this._currentLevel);
    }

    private void Update()
    {
        if (this._dropLevel)
        {
            this._dropLevel = false;
            DropLevel();
        }

    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.LEVEL_CLOSED, HandleLevelClosed);
    }

    private void HandleLevelClosed(object sender, object param)
    {
        this._currentLevel++;
        PlayerPrefs.SetInt("level", this._currentLevel);
    }

    private void DropLevel()
    {
        PlayerPrefs.SetInt("level", 0);
    }
}
