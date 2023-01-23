using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    public static EndGameController Instance { get; private set; }

    [SerializeField]
    private GameObject _diedPanel;
    [SerializeField]
    private Slider _diedSlider;
    [SerializeField]
    private GameObject _sight;

    [SerializeField]
    private GameObject _victimUi;

    public VictimController _victimController;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.SubscribeToEvent(EventType.DEMON_BURNED, ActivateVictimUi);

        Instance = this;   
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.DEMON_BURNED, ActivateVictimUi);
    }

    public void ActivateEndGameUI()
    {
        Cursor.lockState = CursorLockMode.None;

        this._sight.SetActive(false);
        LauncherUi.Instance.SetNewSlider(this._diedSlider);
        this._diedPanel.SetActive(true);
    }

    public void ActivateVictimUi(object sender, object param)
    {
        Cursor.lockState = CursorLockMode.None;

        this._sight.SetActive(false);
        this._victimUi.SetActive(true);
    }

    private void LoadFinalScene()
    {
        LauncherUi.Instance.DisplayLoadingBar();
        Launcher.Instance.PlayFinalScene();
    }

    public void CheckVictimAndEndGame(string name)
    {
        if (this._victimController.CheckVictim(name))
        {
            LoadFinalScene();
        }
        else
        {
            EventBus.Dispath(EventType.KILL_PLAYER, this, this);
        }
    }
}
