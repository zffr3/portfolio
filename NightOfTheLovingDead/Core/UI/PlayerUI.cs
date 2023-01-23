using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance;

    [SerializeField]
    private GameObject _npcInteractionPanel;

    [SerializeField]
    private GameObject _crateUi;

    [SerializeField]
    private GameObject _questUi;

    [SerializeField]
    private Text _qTitleTxt;
    [SerializeField]
    private Text _qFirstAnswer;
    [SerializeField]
    private Text _qSecondAnswer;

    [SerializeField]
    private TMP_Text _bulletCount;
    [SerializeField]
    private TMP_Text _bulletHeapCount;

    [SerializeField]
    private TMP_Text _healthCount;

    [SerializeField]
    private Text _questText;

    [SerializeField]
    private TMP_Text _xpText;

    [SerializeField]
    private TMP_Text _rankText;

    [SerializeField]
    private Image _bloodScreen;

    [SerializeField]
    private GameObject _deathPanel;

    [SerializeField]
    private GameObject _map;

    [SerializeField]
    private GameObject _settings;

    [SerializeField]
    private GameObject _reloadIndicator;

    [SerializeField]
    private GameObject _helpPanel;

    [SerializeField]
    private GameObject _upgradePanel;

    [SerializeField]
    private List<GameObject> _allUiObjects;

    private List<GameObject> _activeObjects;

    [SerializeField]
    private List<GameObject> _interactiveUiObjects;

    private void Awake()
    {
        Instance = this;

        EventBus.SubscribeToEvent(EventType.PLAYER_RANK_UPPED, ShowRankNotification);
        EventBus.SubscribeToEvent(EventType.PLAYER_RANK_UPPED, UpdateRankText);
        EventBus.SubscribeToEvent(EventType.PAUSE_STATE_CHANGED, PauseStateChanged);
    }

    private void OnDisable()
    {
        EventBus.UnsubscribeFromEvent(EventType.PLAYER_RANK_UPPED, ShowRankNotification);
        EventBus.UnsubscribeFromEvent(EventType.PLAYER_RANK_UPPED, UpdateRankText);
        EventBus.UnsubscribeFromEvent(EventType.PAUSE_STATE_CHANGED, PauseStateChanged);
    }

    public void ChangeNpcInteractionPanelActiveState(bool newState)
    {
        this._npcInteractionPanel.SetActive(newState);
    }

    public void ChangeCrateUIActiveState(bool newState)
    {
        this._crateUi.SetActive(newState);
    }

    public void ShowQuestUi(string title, string fAnswer, string sAnswer)
    {
        this._npcInteractionPanel.SetActive(false);
        this._questUi.SetActive(true);
        this._qTitleTxt.text = title;
        this._qFirstAnswer.text = fAnswer;
        this._qSecondAnswer.text = sAnswer;

        CursorController.Instance.UnlockCursor();
    }

    public void CloseQuestUi()
    {
        this._questUi.SetActive(false);
        this._qTitleTxt.text = " ";
        this._qFirstAnswer.text = " ";
        this._qSecondAnswer.text = " ";

        CursorController.Instance.LockCursor();
    }

    public void DisplayBullets(int bullets)
    {
        this._bulletCount.text = bullets.ToString();
    }

    public void DisplayMaxBullets(int bulletHeap)
    {
        this._bulletHeapCount.text = bulletHeap.ToString();
    }

    public void DisplayHealth(float heatlh)
    {
        this._healthCount.text = Mathf.RoundToInt(heatlh).ToString();
    }

    public void ChangeBloodScreenActiveState(bool state)
    {
        this._bloodScreen.gameObject.SetActive(state);
    }

    public void ChangeAlphaBloodScreen(float damgeCount, float currentHealth)
    {
        this._bloodScreen.color = new Color(255, 255, 255, this._bloodScreen.color.a + damgeCount);
        if (currentHealth > 30f)
        {
            StartCoroutine(ActivateWithDelay(this._bloodScreen.gameObject));
        }
        else
        {
            if (!this._bloodScreen.gameObject.activeSelf)
            {
                StopAllCoroutines();
                this._bloodScreen.gameObject.SetActive(true);
            }
        }        
    }

    public void ChangeDeathCamState(bool state)
    {
        this._deathPanel.SetActive(state);
    }

    public void ChangeMapState(bool mapState)
    {
        if (mapState)
        {
            CloseAllPanels();
            CursorController.Instance.UnlockCursor();
        }
        else
        {
            CursorController.Instance.LockCursor();
        }
    }

    public void SetQuestText(string text)
    {
        this._questText.gameObject.SetActive(true);
        this._questText.text = text;
    }

    public void CloseQuestText()
    {
        this._questText.text = "";
        this._questText.gameObject.SetActive(false);

    }

    public void ShowRankNotification(object sender, object rank)
    {
        this.StartCoroutine(DisplayTextOnTopScreen($"{((Ranks)rank).ToString()} rank upped"));
    }

    public void ChangeReloadIndicatorState(bool state)
    {
        this._reloadIndicator.SetActive(state);
    }

    private void PauseStateChanged(object openSettings, object pauseState)
    {
        if ((bool)pauseState)
        {
            this._activeObjects = new List<GameObject>();

            for (int i = 0; i < this._allUiObjects.Count; i++)
            {
                if (this._allUiObjects[i].activeSelf)
                {
                    this._activeObjects.Add(this._allUiObjects[i]);
                    this._allUiObjects[i].SetActive(false);
                }
            }

            CursorController.Instance.UnlockCursor();
        }
        else
        {
            if (this._activeObjects != null)
            {
                for (int i = 0; i < this._activeObjects.Count; i++)
                {
                    this._activeObjects[i].SetActive(true);
                }
            }
            CursorController.Instance.LockCursor();
            CloseAllInteractivePanel();
        }

    }

    public void OpenSettings()
    {
        this._settings.SetActive(true);
    }

    private bool CloseAllInteractivePanel()
    {
        bool panelClosed = false;
        for (int i = 0; i < this._interactiveUiObjects.Count; i++)
        {
            if (this._interactiveUiObjects[i].activeSelf)
            {
                this._interactiveUiObjects[i].SetActive(false);
                panelClosed = true;
            }
        }
        return panelClosed;
    }

    public void UpdateXpText(string newXpData)
    {
        this._xpText.text = newXpData;
    }

    public void UpdateRankText(object sender,object newRankData)
    {
        this._rankText.text = ((Ranks)newRankData).ToString();
    }

    public void CloseHelpPanel()
    {
        this._helpPanel.SetActive(false);
    }

    public void ChangeUpgradePanelState()
    {
        this._upgradePanel.SetActive(true);
    }

    public void CloseAllPanels()
    {
        CloseQuestText();
        CloseQuestUi();
        DisplayBullets(0);
        DisplayMaxBullets(0);
        ChangeNpcInteractionPanelActiveState(false);
        ChangeCrateUIActiveState(false);
        ChangeReloadIndicatorState(false);
        //ChangeUpgradePanelState();
        CloseHelpPanel();
    }

    IEnumerator DisplayTextOnTopScreen(string text)
    {
        if (this._questText != null)
        {
            this._questText.gameObject.SetActive(true);

            this._questText.text = text;
            yield return new WaitForSeconds(this._questText.text.Length / 2);
            this._questText.text = "";

            this._questText.gameObject.SetActive(false);
        }
    }

    IEnumerator ActivateWithDelay(GameObject targerObject)
    {
        targerObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        targerObject.SetActive(false);
    }
}
