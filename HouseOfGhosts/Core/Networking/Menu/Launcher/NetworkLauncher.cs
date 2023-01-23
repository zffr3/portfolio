using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkLauncher : MonoBehaviourPunCallbacks
{
    public static NetworkLauncher Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public void CreateRoom()
    {
        string roomName = NetworkMenuText.Instance.GetRoomName();
        if (!string.IsNullOrEmpty(roomName))
        {
            PhotonNetwork.CreateRoom(roomName);
            MenuNavigation.Instance.OpenMenu(MenuPanelType.Loading);
        }
        else
        {
            return;
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuNavigation.Instance.OpenMenu(MenuPanelType.Loading);
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuNavigation.Instance.OpenMenu(MenuPanelType.Loading);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(3);
    }

    #region PUN_CALLBAKS
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuNavigation.Instance.OpenMenu(MenuPanelType.NetworkTitle);
        PhotonNetwork.NickName = NetworkMenuText.Instance.GetPlayerNick();
    }


    public override void OnJoinedRoom()
    {
        MenuNavigation.Instance.OpenMenu(MenuPanelType.Room);
        NetworkMenuText.Instance.SetRoomNameText(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        NetworkMenuText.Instance.SetErrorText($"Room creation failed: {message}");
        MenuNavigation.Instance.OpenMenu(MenuPanelType.Error);
    }

    public override void OnLeftRoom()
    {
        MenuNavigation.Instance.OpenMenu(MenuPanelType.NetworkTitle);
    }
    #endregion
}
