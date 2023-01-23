using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    public static Launcher Instance;

    [SerializeField]
    private Transform roomListContainer;
    [SerializeField]
    private GameObject roomListItem;

    [SerializeField]
    private Transform playerListContainer;
    [SerializeField]
    private GameObject playerListItem;

    [SerializeField]
    private TMP_InputField userNickInp;

    [SerializeField]
    private GameObject startGameBtn;

    [SerializeField]
    private TMP_Text _roomNameText;

    [SerializeField]
    private TMP_InputField _roomNameInp;

    [SerializeField]
    private GameObject matchTypeGo;
    [SerializeField]
    private Dropdown matchTypeDropDown;

    [SerializeField]
    private TMP_Text _regionTxt;

    [SerializeField]
    private TMP_Dropdown _regionList;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        MenuUi.Instance.ActivatePanel("MenuPanel");
    }

    public void Create()
    {
        SaveSystem.instance.NewGame();
        SceneManager.LoadScene("BigWorld", LoadSceneMode.Single);
    }

    public void LoadWithSave(string saveName)
    {
        SaveSystem.instance.LoadGame(saveName);
        SceneManager.LoadScene("BigWorld", LoadSceneMode.Single);
    }

    public void LeaveRoom()
    {
        MenuUi.Instance.ActivatePanel("LoadScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
