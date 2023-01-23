using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private BulletController _bullet;

    [SerializeField]
    private Transform _muzzle;

    [SerializeField]
    private float _shotPower;

    [SerializeField]
    private Slider _powerMultiplyer;

    [SerializeField]
    private float _shotDelay;
    [SerializeField]
    private bool _canShot;

    private void Awake()
    {
        EventBus.SubscribeToEvent(EventType.SHOT_BTN_PRESSED, Shot);
        EventBus.SubscribeToEvent(EventType.DATA_CB_LOADED, ReciveData);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.SHOT_BTN_PRESSED, Shot);
        EventBus.UnsubscribeFromEvent(EventType.DATA_CB_LOADED, ReciveData);
    }

    private void Start()
    {
        this._shotPower = UpgradeSystem.Instance.GetPowerWalue();
    }

    private void Shot(object sender, object param)
    {
        if (this._canShot)
        {
            GameObject spawnedBullet = Instantiate(this._bullet.CurrentBullet, this._muzzle.position, this._muzzle.rotation);
            Rigidbody bulletBody = spawnedBullet.GetComponent<Rigidbody>();
            bulletBody.mass = UpgradeSystem.Instance.GetWeight();
            bulletBody.AddForce(this._muzzle.forward * this._shotPower * this._powerMultiplyer.value);

            this._canShot = false;
            StartCoroutine(ResetShot());
        }
    }

    private void ReciveData(object sender, object param)
    {
        this._shotPower = (float)sender;
    }

    private IEnumerator ResetShot()
    {
        yield return new WaitForSecondsRealtime(this._shotDelay);
        this._canShot = true;
    }
}
