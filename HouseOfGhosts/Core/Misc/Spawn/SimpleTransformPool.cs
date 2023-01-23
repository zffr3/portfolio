using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTransformPool : MonoBehaviour
{
    public static SimpleTransformPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private List<SimpleTransformContainer> _pool;

    public Transform GetRandomPoint(SimplePoolTypes type)
    {
        for (int i = 0; i < this._pool.Count; i++)
        {
            if (this._pool[i].IsEqualType(type))
            {
                return this._pool[i].GetRandomPoint();
            }
        }

        return null;
    }


    [System.Serializable]
    public class SimpleTransformContainer
    {
        [SerializeField]
        private SimplePoolTypes _currentContainerType;

        [SerializeField]
        private List<Transform> _transformList;

        [SerializeField]
        private bool _useRandomOffset;

        [SerializeField]
        private float _randomOffset;

        public bool IsEqualType(SimplePoolTypes type)
        {
            return this._currentContainerType == type;
        }

        public Transform GetRandomPoint()
        {
            Transform point = this._transformList[Random.Range(0, this._transformList.Count)];

            if (this._useRandomOffset)
            {
                point.position = new Vector3(point.position.x + Random.Range(0, this._randomOffset),point.position.y, point.position.z + Random.Range(0, this._randomOffset));
            }

            return point;
        }
    }
}

public enum SimplePoolTypes
{
    Players,
    Enemys,
    Keys
}
