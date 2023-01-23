using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrlOpen : MonoBehaviour
{
    [SerializeField]
    private string _url;

    public void OpenUrl()
    {
        Application.OpenURL(this._url);
    }
}
