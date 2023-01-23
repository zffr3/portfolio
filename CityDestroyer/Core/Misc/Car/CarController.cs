using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _carPool;

    private List<GameObject> _activatedCars;

    [SerializeField]
    private float _minDelay;
    [SerializeField]
    private float _maxDelay;

    [SerializeField]
    private int _maximumCarCount;

    [SerializeField]
    private List<Color32> _carPaints;

    // Start is called before the first frame update
    void Start()
    {
        this._activatedCars = new List<GameObject>();

        EventBus.SubscribeToEvent(EventType.CAR_ANIM_ENDED, ReturnCarToPool);

        StartCoroutine(CarControllerLoop());
    }

    private void OnDestroy()
    {
        EventBus.SubscribeToEvent(EventType.CAR_ANIM_ENDED, ReturnCarToPool);
    }

    private void ReturnCarToPool(object sender, object param)
    {
        GameObject car = (GameObject)param;
        if (car != null && this._activatedCars.Count != 0)
        {
            if (this._activatedCars.Remove(car) && !this._carPool.Contains(car))
            {
                this._carPool.Add(car);
            }
        }
    }

    IEnumerator CarControllerLoop()
    {
        float loopDelay = Random.Range(this._minDelay, this._maxDelay);
        yield return new WaitForSecondsRealtime(loopDelay);

        int carToSpawnCount = Mathf.Clamp(Random.Range(0, this._maximumCarCount), 0, this._carPool.Count);

        for (int i = 0; i < carToSpawnCount; i++)
        {
            if (this._carPool.Count == 0)
            {
                break;
            }
            int carIndex = Random.Range(0, this._carPool.Count);

            this._carPool[carIndex].GetComponent<MeshRenderer>().material.color = this._carPaints[Random.Range(0, this._carPaints.Count)];
            this._carPool[carIndex].SetActive(true);

            this._activatedCars.Add(this._carPool[carIndex]);

            this._carPool.RemoveAt(carIndex);
        }

        StartCoroutine(CarControllerLoop());
    }
}
