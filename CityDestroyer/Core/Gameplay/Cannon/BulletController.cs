using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private GameObject _normalBullet;
    [SerializeField]
    private GameObject _explosionBullet;

    [SerializeField]
    public GameObject CurrentBullet { get; private set; }

    private void Awake()
    {
        EventBus.SubscribeToEvent(EventType.DATA_CB_LOADED, ReciveData);
        EventBus.SubscribeToEvent(EventType.AD_COMPLETED, TakeRewardForTheAD);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.DATA_CB_LOADED, ReciveData);
        EventBus.UnsubscribeFromEvent(EventType.AD_COMPLETED, TakeRewardForTheAD);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.CurrentBullet = this._normalBullet;
    }

    public void SetExplosionBullet()
    {
        this.CurrentBullet = this._explosionBullet;
    }

    private void ReciveData(object sender, object param)
    {
        this._normalBullet.GetComponent<Rigidbody>().mass = (float)param;
    }

    private void TakeRewardForTheAD(object sender, object param)
    {
        SetExplosionBullet();
    }
}
