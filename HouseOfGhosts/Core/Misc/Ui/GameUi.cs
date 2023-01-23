using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUi : MonoBehaviour
{
    public static GameUi instance;

    [SerializeField]
    private GameObject _takeButton;

    [SerializeField]
    private Slider _staminaSlider;

    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private TMP_Text _notificationText;

    private bool _showStaticText;
    [SerializeField]
    private string _staticTextContent;

    private void Start()
    {
        instance = this;
        EventBus.SubscribeToEvent(EventType.PAUSESTATE_CHANGED, ChangePauseState);
        EventBus.SubscribeToEvent(EventType.CURSEDITEMS_COUNT_CHANGED, StartNotification);
        //EventBus.SubscribeToEvent(EventType.NORMALITEM_TAKED, ShowStaticText);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.PAUSESTATE_CHANGED, ChangePauseState);
        EventBus.UnsubscribeFromEvent(EventType.CURSEDITEMS_COUNT_CHANGED, StartNotification);
        //EventBus.UnsubscribeFromEvent(EventType.NORMALITEM_TAKED, ShowStaticText);
    }

    public void ChangeTakeButtonState(bool state)
    {
        this._takeButton.SetActive(state);
    }

    public void ChangeStaminaValue(float value)
    {
        this._staminaSlider.value += value;
    }

    public void SyncStamina()
    {
        this._staminaSlider.value = 1000;
    }

    private void ChangePauseState(object sender, object param)
    {
        if (this._pauseMenu != null)
        {
            this._pauseMenu.SetActive((bool)param);
        }

        Cursor.lockState = (bool)param == true ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void StartNotification(object sender, object param)
    {
        this._notificationText.gameObject.SetActive(true);
        this._notificationText.text = $"{((int)param).ToString()} cursed items left";

        StartCoroutine(ShowNotification());
    }

    private void ShowStaticText(object sender, object param)
    {
        this._notificationText.gameObject.SetActive(true);
        this._notificationText.text = (string)param;

        StartCoroutine(ShowNotification());
    }

    IEnumerator ShowNotification()
    {
        yield return new WaitForSecondsRealtime(5f);

        this._notificationText.gameObject.SetActive(false);
    }
}
