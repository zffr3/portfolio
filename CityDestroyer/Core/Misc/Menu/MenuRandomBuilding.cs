using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRandomBuilding : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _buildings;

    // Start is called before the first frame update
    void Start()
    {
        this._buildings[Random.Range(0, this._buildings.Count)].SetActive(true);
    }
}
