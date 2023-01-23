using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerContoller : MonoBehaviour
{
    [SerializeField]
    private List<LockerContainer> _lockersGropus;

    [SerializeField]
    private float _activatedLockersInGroup;

    private void Start()
    {
        for (int i = 0; i < this._lockersGropus.Count; i++)
        {
            for (int j = 0; j < this._activatedLockersInGroup; j++)
            {
                GameObject locker = this._lockersGropus[i].Lockers[Random.Range(0, this._lockersGropus[i].Lockers.Count)];
                this._lockersGropus[i].Lockers.Remove(locker);

                locker.SetActive(true);
            }
        }
    }

    [System.Serializable]
    public class LockerContainer
    {
        public List<GameObject> Lockers;
    }
}
