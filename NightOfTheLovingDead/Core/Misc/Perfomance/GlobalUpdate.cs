using UnityEngine;

public class GlobalUpdate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < CachedMonoBehaviour.allUpdates.Count; i++)
        {
            CachedMonoBehaviour.allUpdates[i].Tick();
        }   
    }
}
