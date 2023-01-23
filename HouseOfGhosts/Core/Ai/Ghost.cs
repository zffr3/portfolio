using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GhostMove))]
public class Ghost : MonoBehaviour
{
    [SerializeField]
    private GhostMove _ghostMove;
    [SerializeField]
    private GhostSens _ghostSens;
    [SerializeField]
    private GhostAnimations _ghostAnimations;

    [SerializeField]
    private GraphNode _currentNode;

    [SerializeField]
    private float _changeTargetTimePatrol;
    [SerializeField]
    private float _changeTargetTimeHunt;
    private float _changeTargetTime;

    [SerializeField]
    private GameObject _ghostHumanView;
    [SerializeField]
    private GameObject _ghostDemonView;

    [SerializeField]
    private int _showViewPercent;
    [SerializeField]
    private float _showViewTime;

    private bool _isHunting;
    [SerializeField]
    private bool _canAttackPlayer;
    [SerializeField]
    private bool _isPlayerTarget;

    // Start is called before the first frame update
    void Start()
    {
        this._ghostMove = GetComponent<GhostMove>();
        this._ghostSens = GetComponentInChildren<GhostSens>();    

        this._ghostMove.SetTarget(this._currentNode.transform);

        this._changeTargetTime = this._changeTargetTimePatrol;

        this._isHunting = false;

        this._canAttackPlayer = true;

        StartCoroutine(ChangeGraphNode());
        StartCoroutine(DisplayView());

        EventBus.SubscribeToEvent(EventType.START_HUNTING, StartHunting);
        EventBus.SubscribeToEvent(EventType.STOP_HUNTING, StopHunting);
        EventBus.SubscribeToEvent(EventType.HIDE_PLAER, HandlePlayerHide);
        EventBus.SubscribeToEvent(EventType.RELEASE_PLAYER, HandlePlayerRelease);
        EventBus.SubscribeToEvent(EventType.KILL_DEMON, KillDemon);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.START_HUNTING, StartHunting);
        EventBus.UnsubscribeFromEvent(EventType.STOP_HUNTING, StopHunting);
        EventBus.UnsubscribeFromEvent(EventType.HIDE_PLAER, HandlePlayerHide);
        EventBus.UnsubscribeFromEvent(EventType.RELEASE_PLAYER, HandlePlayerRelease);
        EventBus.UnsubscribeFromEvent(EventType.KILL_DEMON, KillDemon);
    }

    private void DisableView()
    {
        this._ghostHumanView?.SetActive(false);
        this._ghostDemonView?.SetActive(false);
    }

    private void DisplayDemonView()
    {
        DisableView();
        this._ghostDemonView.SetActive(true);

        this._ghostAnimations.SetNewAnimator(this._ghostDemonView.GetComponent<Animator>());
        this._ghostAnimations.ChangeDemonVariable(true);
    }

    private void CloseDemonView()
    {
        DisableView();

        this._ghostAnimations.SetNewAnimator(this._ghostHumanView.GetComponent<Animator>());
        this._ghostAnimations.ChangeDemonVariable(false);
    }

    public void KillPlayer()
    {
        if (this._canAttackPlayer)
        {
            EventBus.Dispath(EventType.KILL_PLAYER, this, this);
        }
        else
        {
            this._ghostMove.SetTarget(null);
            LostPlayer();
        }

    }

    private void KillDemon(object sender, object param)
    {
        Destroy(this.gameObject);
    }

    public void SetStartNode(GraphNode node)
    {
        this._currentNode = node;
        this._ghostMove.SetTarget(this._currentNode.transform);
    }

    public void SetPlayerAsTarget(Transform playerTransform)
    {
        if (this._canAttackPlayer)
        {
            this._isPlayerTarget = true;
            this._ghostMove.SetTarget(playerTransform);
        }
    }

    public void LostPlayer()
    {
        this._isPlayerTarget = false;
        this._ghostMove.SetTarget(this._currentNode.transform);
    }

    private void StartHunting(object param, object sender)
    {
        StopAllCoroutines();
        this._isHunting = true;

        DisplayDemonView();
        this._changeTargetTime = this._changeTargetTimeHunt;
        this._ghostSens.ChangeHuntingState(true);

        StartCoroutine(ChangeGraphNode());
    }

    private void StopHunting(object param, object sender)
    {
        this._isHunting = false;

        CloseDemonView();
        this._changeTargetTime = this._changeTargetTimePatrol;
        this._ghostSens.ChangeHuntingState(false);

        StartCoroutine(DisplayView());
    }

    private void HandlePlayerHide(object sender, object param)
    {
        this._canAttackPlayer = false;
    }

    private void HandlePlayerRelease(object sender, object param)
    {
        this._canAttackPlayer = true;
    }

    IEnumerator ChangeGraphNode()
    {
        yield return new WaitForSeconds(this._changeTargetTime);
        if (!this._isPlayerTarget)
        {
            this._currentNode = this._currentNode.GetNextRandomNode();

            if (this._ghostMove != null)
            {
                this._ghostMove.SetTarget(this._currentNode.transform);
            }

            StartCoroutine(ChangeGraphNode());
        }
    }

    IEnumerator DisplayView()
    {
        if (!this._isHunting)
        {
            DisableView();
            yield return new WaitForSeconds(this._showViewTime);

            int randomSeed = Random.Range(0, 100);
            if (randomSeed <= this._showViewPercent)
            {
                this._ghostHumanView.SetActive(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(this._showViewTime);
        }

        StartCoroutine(DisplayView());
    }
}
