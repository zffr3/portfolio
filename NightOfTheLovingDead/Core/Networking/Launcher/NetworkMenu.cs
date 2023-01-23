using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkMenu : MonoBehaviour
{
    public void LeaveFromRoom()
    {
        Destroy(RoomManager.Instance.gameObject);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
