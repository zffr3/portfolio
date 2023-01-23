using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameLogo : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer _playerSrc;

    [SerializeField]
    private GameObject _playerParent;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CloseVideoLogo());
    }

    IEnumerator CloseVideoLogo()
    {
        yield return new WaitForSecondsRealtime((float)this._playerSrc.clip.length);

        this._playerParent.SetActive(false);
        EventBus.Dispath(EventType.GAMELOGO_CLOSED, this, this);
    }
}
