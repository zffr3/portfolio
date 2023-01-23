using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfomanceChunk : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _visualObjects;

    [SerializeField]
    private bool _isMainChunk;

    private void Start()
    {
        if (!this._isMainChunk)
        {
            ChangeChunkState(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() == null)
            return;

        ChangeChunkState(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() == null)
            return;

        ChangeChunkState(false);
    }

    private void ChangeChunkState(bool state)
    {
        for (int i = 0; i < this._visualObjects.Count; i++)
        {
            this._visualObjects[i].SetActive(state);
            MeshRenderer[] renderSource = this._visualObjects[i].GetComponentsInChildren<MeshRenderer>();
            if (renderSource != null)
            {
                for (int j = 0; j < renderSource.Length; j++)
                {
                    renderSource[j].enabled = state;
                }
            }
        }
    }
}
