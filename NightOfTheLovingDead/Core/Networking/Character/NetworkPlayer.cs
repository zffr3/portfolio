using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    public static NetworkPlayer NetworkPlayerInstance;

    [SerializeField]
    private PlayerStats _stats;

    [SerializeField]
    private string _playerNickName;
    [SerializeField]
    private PlayerCharacterType _selectedCharacterType;

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private GameObject _playerController;

    [SerializeField]
    private List<MonoBehaviour> _ownerComponent;

    // Start is called before the first frame update
    void Start()
    {
        this._stats = this.GetComponent<PlayerStats>();

        NetworkPlayerInstance = this;
        this._selectedCharacterType = PlayerCharacterType.NewHuman;
        CreateController();

        NoldGameLoop.Instance.AddPlayerToList(this);
    }

    private void OnDestroy()
    {
        NoldGameLoop.Instance.RemovePlayerFromList(this);
    }

    public void CreateController()
    {
        PlayerUI.Instance.ChangeBloodScreenActiveState(false);
        DeathCam.Instance.DisableDeathCam();
        PlayerUI.Instance.ChangeDeathCamState(false);
        Transform spawnPos = SpawnManager.Instance.GetSpawnpoint();
        this._playerController = Instantiate(this._playerPrefab, spawnPos.position, spawnPos.rotation);

        this._playerController.GetComponent<NetworkCharacter>().ConfigureCharacter(this._stats.GetCharacterMovementData(), this._playerNickName, this._selectedCharacterType);
        this._playerController.GetComponent<PlayerHealth>().Initialize(PlayerStats.Instance.GetMaxPlayerHealth());
        CursorController.Instance.LockCursor();
    }

    public void Die()
    {
        PlayerUI.Instance.ChangeBloodScreenActiveState(false);
        PlayerUI.Instance.ChangeDeathCamState(true);
        PlayerUI.Instance.CloseAllPanels();

        DeathCam.Instance.EnableDeathCam();
        GameObject.Destroy(this._playerController);

        CursorController.Instance.UnlockCursor();

    }

    public Transform GetCharacterTransform()
    {
        return this._playerController.transform;
    }

    public void SetCharacterTransform(Vector3 position, Quaternion rotation)
    {
        this._playerController.SetActive(false);

        this._playerController.transform.position = position;
        this._playerController.transform.rotation = rotation;

        this._playerController.SetActive(true);
    }

    private void AddExp(int xp)
    {
        this.GetComponent<PlayerStats>().AddXp(xp);
    }

    public void TakeReward(int reward)
    {
        AddExp(reward);
    }
    public void CallChangeMapState(bool nState)
    {
        this._playerController.GetComponentInChildren<NetworkCharacter>().ChangeCameraState(nState);
    }

    public bool IsEqualNickName(string nickName)
    {
        return nickName == this._playerNickName;
    }

    public void TeleportController(Vector3 position)
    {
        this._playerController.GetComponent<NetworkCharacter>().SetNewState(false);
        this._playerController.transform.position = position;
        this._playerController.GetComponent<NetworkCharacter>().SetNewState(true);
    }

    public string GetNickName()
    {
        return this._playerNickName;
    }
}
public enum PlayerCharacterType
{
    Human,
    NewHuman,
    Zombie
}