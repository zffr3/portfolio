using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField]
    private GameObject _playerInstance;
    [SerializeField]
    private PlayerInventory _inventory;

    [SerializeField]
    private bool _hiden;

    [SerializeField]
    private PlayerState _currentState;

    [SerializeField]
    private GameObject _rayTarget;

    [SerializeField]
    private GameObject _deathRoom;

    [SerializeField]
    private GameObject _pauseCamera;

    [SerializeField]
    private List<MonoBehaviour> _rootComponents;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (this._playerInstance != null)
        {
            this._inventory = this._playerInstance.GetComponent<PlayerInventory>();
        }

        this._currentState = PlayerState.DEFAULT;

        EventBus.SubscribeToEvent(EventType.KILL_PLAYER, KillCharacter);
        EventBus.SubscribeToEvent(EventType.INTERACT_PERFORMED, HandleInteractButton);
        EventBus.SubscribeToEvent(EventType.PLAYER_STATAE_CHANGED, OnStateChanged);
        EventBus.SubscribeToEvent(EventType.PAUSESTATE_CHANGED, PauseStateChanged);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.KILL_PLAYER, KillCharacter);
        EventBus.UnsubscribeFromEvent(EventType.INTERACT_PERFORMED, HandleInteractButton);
        EventBus.UnsubscribeFromEvent(EventType.PLAYER_STATAE_CHANGED, OnStateChanged);
        EventBus.UnsubscribeFromEvent(EventType.PAUSESTATE_CHANGED, PauseStateChanged);
    }

    private void HidePlayer(LockerLook lookSrc)
    {
        this._hiden = true;
        this._playerInstance.SetActive(false);

        EventBus.Dispath(EventType.HIDE_PLAER, this, lookSrc);
    }

    private void ReleasePlayer()
    {
        this._hiden = false;
        this._playerInstance.SetActive(true);

        EventBus.Dispath(EventType.RELEASE_PLAYER, this, this);
    }

    public void SetRayTarget(GameObject target)
    {
        this._rayTarget = target;
    }

    private void KillCharacter(object sender, object param)
    {
        this._playerInstance.GetComponent<Character>().KillPlayer();
        this._deathRoom.SetActive(true);
    }

    private void HandleInteractButton(object sender, object param)
    {
        if (this._hiden)
        {
            ReleasePlayer();
            return;
        }

        if (this._rayTarget != null)
        {
            if ((this._rayTarget.GetComponent<HideSystem>() || this._rayTarget.GetComponentInParent<HideSystem>()) && this._currentState == PlayerState.NEAR_LOKER)
            {
                LockerLook lockerLook = this._rayTarget.GetComponent<LockerLook>();
                if (lockerLook == null)
                {
                    lockerLook = this._rayTarget.GetComponentInParent<LockerLook>();
                }

                HidePlayer(lockerLook);
                return;
            }

            if (this._rayTarget.GetComponent<Radio>() != null && this._currentState == PlayerState.NEAR_RADIO)
            {
                this._rayTarget.GetComponent<Radio>().ActiveRadio();
                return;
            }

            if (this._rayTarget.GetComponent<DoorScript>() != null)
            {
                this._rayTarget.GetComponent<DoorScript>().OpenDoor();
            }

            if (this._rayTarget.GetComponent<NormalItem>() != null || this._rayTarget.GetComponent<CurvedItem>() != null)
            {
                EventBus.Dispath(EventType.NORMALITEM_TAKED, this, "Find the altar");

                this._inventory.AddItem(this._rayTarget);
                this._rayTarget = null;
            }
        }
    }

    private void OnStateChanged(object sender, object param)
    {
        this._currentState = (PlayerState)param;
    }


    private void PauseStateChanged(object sender, object param)
    {
        if (this._pauseCamera != null)
        {
            this._playerInstance.SetActive(!(bool)param);
            this._pauseCamera.SetActive((bool)param);
        }
    }

}
public enum PlayerState
{
    DEFAULT,
    HIDEN,
    NEAR_LOKER,
    NEAR_RADIO
}
