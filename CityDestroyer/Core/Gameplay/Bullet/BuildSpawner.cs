using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _buildings;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.SubscribeToEvent(EventType.LEVEL_IND_LOADED, SpawnBuilding);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.LEVEL_IND_LOADED, SpawnBuilding);
    }

    private void SpawnBuilding(object sender, object param)
    {
        int buildIndex = Mathf.Clamp((int)param,0, this._buildings.Count-1);
        this._buildings[buildIndex].SetActive(true);

        EventBus.Dispath(EventType.BUILDING_INITIALIZED, this, this._buildings[buildIndex].GetComponentsInChildren<MeshRenderer>().Length);
    }
}
