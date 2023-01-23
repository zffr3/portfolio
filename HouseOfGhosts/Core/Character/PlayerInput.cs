using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private CharacterInput _inputControls;
    private CharacterInput.CharacterActions _actions;

    [SerializeField]
    private FirstPersonController _fpController;
    [SerializeField]
    private CameraLook _camLook;

    private LockerLook _lockerLook;

    private Vector2 _horizontalInput;
    private Vector2 _mouseInput;

    private bool _playerHiden;

    private bool _gamePaused;

    private void Awake()
    {
        this._gamePaused = false;

        this._inputControls = new CharacterInput();
        this._actions = this._inputControls.Character;

        this._actions.Movement.performed += ctx => this._horizontalInput = ctx.ReadValue<Vector2>();
        this._actions.MouseX.performed += ctx => this._mouseInput.x = ctx.ReadValue<float>();
        this._actions.MouseY.performed += ctx => this._mouseInput.y = ctx.ReadValue<float>();
        this._actions.LeftItem.performed += _ => LeftItemPressed();
        this._actions.RightItem.performed += _ => RightItemPressed();
        this._actions.Interact.performed += _ => InteractButtonPressed();
        this._actions.Pause.performed += _ =>  PausePressed();

        EventBus.SubscribeToEvent(EventType.HIDE_PLAER, HandlePlayerHide);
        EventBus.SubscribeToEvent(EventType.RELEASE_PLAYER, HandlePlayerRelease);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.HIDE_PLAER, HandlePlayerHide);
        EventBus.UnsubscribeFromEvent(EventType.RELEASE_PLAYER, HandlePlayerRelease);
    }

    private void Update()
    {
        this._fpController.ReceiveInput(this._horizontalInput, this._actions.Sprint.ReadValue<float>() != 0);

        EventBus.Dispath(EventType.INTERACT_BUTTON_VALUE, this, this._actions.Interact.ReadValue<float>() != 0);

        if (this._playerHiden && this._lockerLook != null)
        {
            this._lockerLook.ReceiveInput(this._mouseInput);
        }
        else
        {
            this._camLook.ReceiveInput(this._mouseInput);
        }

    }

    private void OnEnable()
    {
        this._inputControls.Enable();
    }

    private void OnDisable()
    {
        this._inputControls.Disable();
    }

    public void LeftItemPressed()
    {
        EventBus.Dispath(EventType.LEFTITEM_PRESSED, this,this);
    }

    public void RightItemPressed()
    {
        EventBus.Dispath(EventType.RIGHTITEM_PRESSED, this, this);
    }

    public void InteractButtonPressed()
    {
        EventBus.Dispath(EventType.INTERACT_PERFORMED, this,this);
    }

    private void HandlePlayerHide(object sender, object param)
    {
        this._playerHiden = true;
        this._lockerLook = param as LockerLook;
    }

    private void HandlePlayerRelease(object sender, object param)
    {
        this._playerHiden = false;
        this._lockerLook = null;
    }

    private void PausePressed()
    {
        this._gamePaused = !this._gamePaused;
        EventBus.Dispath(EventType.PAUSESTATE_CHANGED, this,this._gamePaused);
    }
}
