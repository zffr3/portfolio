using System;
using UnityEngine;

public class PlayerKeys : MonoBehaviour
{
    private PlayerInput _inputActions;

    public static PlayerKeys Instance;

    public event  Action<bool> RunBtnStateChanged;

    public event Action ShotBtnPressed;
    public event Action ShotBtnReleased;

    public event Action JumpBtnPressed;

    public event Action NextItmPressed;
    public event Action PrevItmPressed;
                  
    public event Action ReloadBtnPressed;

    public event Action<bool> PauseStateChanged;

    public event Action InteractionBtnPressed;

    public event Action<bool> _mapBtnPressed;

    [SerializeField]
    private bool _isPause;
    private bool _isMapOpened;

    private bool _shifted;
    public bool Shifted { get { return this._shifted; } }

    private bool _takeWeapon;
    public bool TakeWeapon { get { return this._takeWeapon; } }

    private bool _interact;
    public bool Interact { get { return this._interact; } }

    private float _horizontal;
    public float Horizontal { get { return this._horizontal; } }

    private float _vertical;
    public float Vertical { get { return this._vertical; } }

    private float _shotValue;
    public float ShotValue { get { return this._shotValue; } }

    private float _mouseX;
    public float MouseX { get { return this._mouseX;} }

    private float _mouseY;
    public float MouseY { get { return this._mouseY;} }

    private Vector2 _lookVector;
    public Vector2 MouseLookVector { get { return this._lookVector; } }

    private void OnEnable()
    {
        this._inputActions.Enable();
    }

    private void OnDisable()
    {
        this._inputActions.Disable();
        GlobalMap.Instance.UnsubscribeToEventsFromPlayerKeys();
    }

    private void Awake()
    {
        this._inputActions = new PlayerInput();

        this._isPause = false;
        this._isMapOpened = false;
        this._shifted = false;

        this._takeWeapon = false;
        this._interact = false;

        Instance = this;

        GlobalMap.Instance.SubscribeToEventsFromPlayerKeys();

        this._inputActions.Player.LeftBtn.performed += context => this.NextItmPressed?.Invoke();
        this._inputActions.Player.RightBtn.performed += context => this.PrevItmPressed?.Invoke();
        this._inputActions.Player.Reload.performed += context => this.ReloadBtnPressed?.Invoke();
        this._inputActions.Player.InteractionBtn.performed += context => this.InteractionBtnPressed?.Invoke();
        this._inputActions.Player.Jump.performed += context => this.JumpBtnPressed?.Invoke();

        this._inputActions.Player.PauseBtn.performed += context => CallOpenSettings();
        this._inputActions.Player.MapBtn.performed += context => ChangeMapState();

        this._inputActions.Player.Shoot.performed += context => this.ShotBtnPressed?.Invoke();

        this._inputActions.Player.CloseBtn.performed += context=> PlayerUI.Instance.CloseHelpPanel();

        this._inputActions.Player.UpgradeBtn.performed += context => CallUpgradePanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this._isPause)
        {
            Vector2 direction = this._inputActions.Player.Move.ReadValue<Vector2>();    

            this._horizontal = direction.x;
            this._vertical = direction.y;

            this._lookVector = this._inputActions.Player.Look.ReadValue<Vector2>();

            this._mouseX = this._inputActions.Player.CameraAxesX.ReadValue<float>();
            this._mouseY = this._inputActions.Player.CameraAxesY.ReadValue<float>();

            this._takeWeapon = this._inputActions.Player.TakeBtn.ReadValue<float>() != 0;
            this._interact = this._inputActions.Player.InteractionBtn.ReadValue<float>() != 0;

            this._shotValue = this._inputActions.Player.Shoot.ReadValue<float>();

            this._shifted = this._inputActions.Player.Sprint.ReadValue<float>() != 0;
        }
        else
        {
            this._shotValue = 0;
        }
    }

    private void ChangePauseState()
    {
        if (this._isMapOpened)
            return;
        this._isPause = !this._isPause;
        EventBus.Dispath(EventType.PAUSE_STATE_CHANGED, this, this._isPause);
    }

    private void CallUpgradePanel()
    {
        ChangePauseState();
        if (this._isPause)
            PlayerUI.Instance.ChangeUpgradePanelState();
    }

    private void CallOpenSettings()
    {
        ChangePauseState();
        if (this._isPause)
            PlayerUI.Instance.OpenSettings();;
    }

    private void ChangeMapState()
    {
        if (this._isPause)
            return;

        this._isMapOpened = !this._isMapOpened;
        this._mapBtnPressed?.Invoke(this._isMapOpened);
    }

    public void ClearShootEvent()
    {
        this.ShotBtnPressed = null;
        this.ShotBtnReleased = null;
    }
}
