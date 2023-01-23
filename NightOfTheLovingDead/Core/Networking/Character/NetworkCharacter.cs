using System.Collections.Generic;
using UnityEngine;

public class NetworkCharacter : MonoBehaviour
{
    [SerializeField]
    private Camera _eyeCamera;

    [SerializeField]
    private List<MonoBehaviour> _ownerComponents;
    [SerializeField]
    private List<GameObject> _ownerObjects;

    [SerializeField]
    private List<MonoBehaviour> _pauseComponents;

    [SerializeField]
    private GameObject _pauseGameObject;

    [SerializeField]
    private List<GameObject> _visualObject;

    [SerializeField]
    private string _playerName;

    [SerializeField]
    private bool _mapOpened;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this._visualObject.Count; i++)
            this._visualObject[i].SetActive(false);

        EventBus.SubscribeToEvent(EventType.PAUSE_STATE_CHANGED, PauseStateChanged);
    }

    private void OnDisable()
    {
        EventBus.UnsubscribeFromEvent(EventType.PAUSE_STATE_CHANGED, PauseStateChanged);
    }

    public void ConfigureCharacter(float[] data, string playerName, PlayerCharacterType characterType)
    {
        switch (characterType)
        {
            case PlayerCharacterType.Human:
                if (data.Length == 3)
                    this.GetComponent<PlayerMovement>().ConfigurateDat(data[0], data[1], data[2]);
                break;
            default:
                if (data.Length == 3)
                    this.GetComponent<StarterAssets.FirstPersonController>().ConfigurateDat(data[0], data[1], data[2]);
                break;
        }
    }

    public void Teleport(Vector3 position)
    {
        this.transform.position = position;
    }

    public bool CheckPlayerName(string pName)
    {
        return this._playerName == pName;
    }

    public string GetPlayerName()
    {
        return this._playerName;
    }

    public bool DiscrabXp(int cost)
    {
        return PlayerStats.Instance.DiscrabXp(cost);
    }

    public void SetNewState(bool newState)
    {
        this.gameObject.SetActive(newState);
    }

    private void PauseStateChanged(object sender,object pause)
    {
        for (int i = 0; i < this._pauseComponents.Count; i++)
            this._pauseComponents[i].enabled = !(bool)pause;

        this._pauseGameObject.SetActive(!(bool)pause);

        if ((bool)pause)
            CursorController.Instance.UnlockCursor();
        else
            CursorController.Instance.LockCursor();
    }

    public void ChangeCameraState(bool mapState)
    {
        this._eyeCamera.enabled = !mapState;
    }
}
