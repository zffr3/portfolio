using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUiButton : MonoBehaviour
{
    [SerializeField]
    private Sprite _defaultImage;
    [SerializeField]
    private Sprite _completedImage;

    [SerializeField]
    private Image _buttonImageSource;

    [SerializeField]
    private int _upgradeCost;
    [SerializeField]
    private float  _priceIncreasePercentage;
    [SerializeField]
    private TMP_Text _costDisplayedText;

    [SerializeField]
    private UpgradeType _currentUpgradeType;
    [SerializeField]
    private UpgradeStage _currentUpgradeStage;

    [SerializeField]
    private bool _completed;

    private void Awake()
    {
        if (this._costDisplayedText == null)
            this._costDisplayedText = this.GetComponentInChildren<TMP_Text>();

        if (this._buttonImageSource == null)
            this._buttonImageSource = this.GetComponent<Image>();

        this._costDisplayedText.text = this._upgradeCost.ToString();

        EventBus.SubscribeToEvent(EventType.PLAYER_RANK_UPPED, ResetButton);
    }
     
    private void OnDisable()
    {
        EventBus.UnsubscribeFromEvent(EventType.PLAYER_RANK_UPPED, ResetButton);
    }

    public void Upgrade()
    {
        if (!this._completed)
        {
            if (UpgradeSystem.instance != null && UpgradeSystem.instance.Upgrade(this._upgradeCost, this._currentUpgradeType, this._currentUpgradeStage))
            {
                this._completed = true;
                this._buttonImageSource.sprite = this._completedImage;
            }
        }
    }

    private void ResetButton(object sender, object param)
    {
        this._completed = false;
        this._buttonImageSource.sprite = this._defaultImage;

        this._upgradeCost += (int)(this._upgradeCost * this._priceIncreasePercentage);
    }
}
public enum UpgradeType
{
    Weapon,
    Health,
    Power
}

public enum UpgradeStage
{
    Stage1,
    Stage2,
    Stage3
}