using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private int _poolSize;

    [SerializeField]
    private GameObject _targetPoolPrefab;

    [SerializeField]
    private List<MonoBehaviour> _inactivePool;
    [SerializeField]
    private List<MonoBehaviour> _activePool;

    public void InitializePool<T>() where T : MonoBehaviour
    {
        this._inactivePool = new List<MonoBehaviour>();
        this._activePool = new List<MonoBehaviour>();

        for (int i = 0; i < this._poolSize; i++)
        {
            GameObject poolInstance = Instantiate(this._targetPoolPrefab);
            poolInstance.SetActive(false);

            this._inactivePool.Add(poolInstance.GetComponent<T>());
        }
    }

    public T GetObjectFromPool<T>() where T : MonoBehaviour
    {
        if (this._inactivePool.Count == 0)
        {
            InitializePool<T>();
        }
        
        T instanceFromPool = this._inactivePool[Random.Range(0, this._inactivePool.Count)] as T;

        this._activePool.Add(instanceFromPool);
        this._inactivePool.Remove(instanceFromPool);

        return instanceFromPool;
    }

    public void ReturnObjectToPool<T>(T poolInstance) where T : MonoBehaviour
    {
        if (this._activePool == null)
        {
            return;
        }


        this._activePool.Remove(poolInstance);
        this._inactivePool.Add(poolInstance);
    }

    public void ChangeActiveObjectState(bool nState)
    {
        for (int i = 0; i < this._activePool.Count; i++)
        {
            this._activePool[i].gameObject.SetActive(nState);
        }

    }
}
