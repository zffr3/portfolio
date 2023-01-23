using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCam : MonoBehaviour
{
    public static DeathCam Instance;

    private void Start()
    {
        Instance = this;
    }

    public void EnableDeathCam()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableDeathCam()
    {
        this.gameObject.SetActive(false);
    }
}
