using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BunkerEnter : MonoBehaviour, IInteractble
{
    public void OnInteract(Collider sender)
    {
        SaveSystem.instance.NewGame();
        SceneManager.LoadScene("Bunker", LoadSceneMode.Single);
    }
}
