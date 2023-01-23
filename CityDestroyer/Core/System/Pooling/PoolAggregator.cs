using UnityEngine;

public class PoolAggregator : MonoBehaviour
{
    public static PoolAggregator Instance;

    [SerializeField]
    private ObjectPool _gameObjectPool;
    [SerializeField]
    private ObjectPool _transformPool;
    [SerializeField]
    private ObjectPool _vector3Pool;

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
        if (typeof(T) == typeof(GameObject))
            return  this._gameObjectPool;
        else if (typeof(T) == typeof(Transform))
             return this._transformPool;
        else
           return this._vector3Pool;
    }
}
