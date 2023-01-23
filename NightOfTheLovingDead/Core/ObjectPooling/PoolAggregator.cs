using UnityEngine;

public class PoolAggregator : MonoBehaviour
{
    public static PoolAggregator Instance;

    [SerializeField]
    private ObjectPool _walkingZombiePool;
    [SerializeField]
    private ObjectPool _midleZombiePool;
    [SerializeField]
    private ObjectPool _weaponCratePool;

    private void Awake()
    {
        Instance = this;
    }

    public T GetObjectFromPool<T>() where T : MonoBehaviour
    {
        ObjectPool targetPool = DeterminePool<T>();
        return  targetPool.GetObjectFromPool<T>();
    }

    public void ReturnObjectToPool<T>(T targetObject) where T : MonoBehaviour
    {
        ObjectPool targetPool = DeterminePool<T>();
        targetPool.ReturnObjectToPool<T>(targetObject);
    }

    public void ChangeActiveObjectState<T>(bool state) where T : MonoBehaviour
    {
        ObjectPool targetPool = DeterminePool<T>();
        targetPool.ChangeActiveObjectState(state);
    }

    private ObjectPool DeterminePool<T>()
    {
        if (typeof(T) == typeof(WalkingZombieAI))
            return  this._walkingZombiePool;
        else if (typeof(T) == typeof(MidleZombieAI))
             return this._midleZombiePool;
        else
           return this._weaponCratePool;
    }
}
