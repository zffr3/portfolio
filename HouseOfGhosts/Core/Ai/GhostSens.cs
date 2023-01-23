using System.Collections;
using UnityEngine;

public class GhostSens : MonoBehaviour
{
    [SerializeField]
    private Ghost _ghostSrc;

    [SerializeField]
    private float _killDistance;

    private GameObject _playerObj;

    [SerializeField]
    private bool _canKill;
    private bool _isHunting;

    [SerializeField]
    private bool _canAttackPlayer;

    [SerializeField]
    private float _screamDistance;

    [SerializeField]
    private GhostSound _soundController;

    private void Start()
    {
        this._soundController= this.GetComponent<GhostSound>();
        this._ghostSrc = this.GetComponentInParent<Ghost>();
        this._canKill = true;
        this._canAttackPlayer = true;
        EventBus.SubscribeToEvent(EventType.HIDE_PLAER, HandlePlayerHide);
        EventBus.SubscribeToEvent(EventType.RELEASE_PLAYER, HandlePlayerRelease);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.HIDE_PLAER, HandlePlayerHide);
        EventBus.UnsubscribeFromEvent(EventType.RELEASE_PLAYER, HandlePlayerRelease);
    }

    void Update()
    {
        if (!this._isHunting)
        {
            return;
        }

        if (this._playerObj != null && this._canKill)
        {
            float distnaceNearPlayer = Vector3.Distance(this.transform.position, this._playerObj.transform.position);

            if (distnaceNearPlayer <= this._screamDistance && this._canKill)
            {
                this._soundController.Scream();
            }
            else
            {
                this._soundController.AllowMetalDrop();
            }

            if ( distnaceNearPlayer <= this._killDistance)
            {
                this._ghostSrc.KillPlayer();
                this._canKill = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Character>() != null)
        {
            if (this._isHunting && this._canAttackPlayer)
            {

                this._playerObj = other.gameObject;
                this._ghostSrc.SetPlayerAsTarget(other.transform);
            }
            if (!this._isHunting)
            {
                EventBus.Dispath(EventType.GHOSTSENS_PLAYERSTATUS_CHANGED, this, true);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            if (this._isHunting)
            {
                this._playerObj = null;
                this._ghostSrc.LostPlayer();
            }
            if (!this._isHunting)
            {
                EventBus.Dispath(EventType.GHOSTSENS_PLAYERSTATUS_CHANGED, this, false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (this._isHunting && this._canAttackPlayer)
        {
            if (other.GetComponent<Character>() != null)
            {
                this._playerObj = other.gameObject;
                this._ghostSrc.SetPlayerAsTarget(other.transform);
            }
        }
    }

    public void ChangeHuntingState(bool huntingState)
    {
        this._isHunting = huntingState;
    }

    private void HandlePlayerHide(object sender, object param)
    {
        this._canAttackPlayer = false;
    }

    private void HandlePlayerRelease(object sender, object param)
    {
        this._canAttackPlayer = true;
        this._canKill = true;
    }
}
