using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartGameWithTrailer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _targetObjects;

    [SerializeField]
    private GameObject _logo;

    private void Awake()
    {
        StartCoroutine(ActivateObjects());
    }

    IEnumerator ActivateObjects()
    {
        yield return new WaitForSeconds(8);
        for (int i = 0; i < this._targetObjects.Count; i++)
            this._targetObjects[i].SetActive(true);

        this._logo.SetActive(false);
    }
}
