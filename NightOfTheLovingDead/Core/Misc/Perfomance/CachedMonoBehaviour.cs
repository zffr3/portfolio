using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CachedMonoBehaviour : MonoBehaviour
{
    public static List<CachedMonoBehaviour> allUpdates = new List<CachedMonoBehaviour>(10001);

    protected Transform cachedTransform;

    private void Start()
    {
        this.cachedTransform = this.GetComponent<Transform>();
    }

    private void OnEnable()
    {
        allUpdates.Add(this);
    }

    private void OnDisable()
    {
        RemoveFromCache();
    }

    private void OnDestroy()
    {
        RemoveFromCache();
    }

    private void RemoveFromCache()
    {
        allUpdates.Remove(this);
    }

    public void Tick()
    {
        OnTick();
    }

    public virtual void OnTick()
    {

    }
}
