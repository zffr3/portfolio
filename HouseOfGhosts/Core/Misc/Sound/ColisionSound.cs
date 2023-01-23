using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;

    private bool _played;

    private void Start()
    {
        this._played = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!this._played)
        {
            this._source.Play();
            this._played = true;
        }
    }
}
