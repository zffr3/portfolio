using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RayFire;

[RequireComponent(typeof(Recoil))]
public class WeaponData : MonoBehaviour
{
    private Recoil _recoilSrc;

    [SerializeField]
    private string _weaponName;

    [SerializeField]
    private WeaponType _type;

    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private Transform _startPosition;
    [SerializeField]
    private float _speed;

    [SerializeField]
    private int _maxBulletCount;

    [SerializeField]
    private int _currentBulletCount;

    [SerializeField]
    private int _bulletHipCount;

    [SerializeField]
    private int _maxHipCount;

    [SerializeField]
    private float _shootDistance;

    [SerializeField]
    private int _damge;

    [SerializeField]
    private float _shotDelay;

    [SerializeField]
    private bool _isAutomaticWeapon;

    [SerializeField]
    private LayerMask _bulletMask;

    [SerializeField]
    private WeaponAnimations _animSrc;

    [SerializeField]
    private RayfireGun _gun;
    [SerializeField]
    private Transform _rfTarget;

    private delegate bool ShootDlg();
    private ShootDlg shootFromWeapon;
    private bool _isShooting;
    private bool _shooted;

    private void Start()
    {
        this._recoilSrc = this.GetComponent<Recoil>();
       
    }

    private void OnEnable()
    {
        LoadSettings();

        UpdateBulletInfo();
        PlayerUI.Instance.ChangeReloadIndicatorState(false);
        if (this._isAutomaticWeapon)
        {
            this._isShooting = false;
            PlayerKeys.Instance.ShotBtnPressed += () => { this._isShooting = true; StartCoroutine(ShootWithDelay()); };
            PlayerKeys.Instance.ShotBtnReleased += () => { this._isShooting = false; };
        }
        else
        {
            PlayerKeys.Instance.ShotBtnPressed += CallSingleShoot;
        }
    }

    private void OnDisable()
    {
        PlayerKeys.Instance.ClearShootEvent();
    }

    private void Update()
    {
        this._isShooting = PlayerKeys.Instance.ShotValue != 0;
    }

    public string GetWeaponName()
    {
        return this._weaponName;
    }

    public WeaponType GetWeaponType()
    {
        return this._type;
    }

    public void SetWeaponClip()
    {
        PlayerKeys.Instance.ClearShootEvent();

        if (this._isAutomaticWeapon)
        {
            this._isShooting = false;
            PlayerKeys.Instance.ShotBtnPressed += () => { this._isShooting = true; StartCoroutine(ShootWithDelay());};
            PlayerKeys.Instance.ShotBtnReleased += () => { this._isShooting = false; };  
        }
        else
        {
            PlayerKeys.Instance.ShotBtnPressed += CallSingleShoot;
        }

        LoadSettings();

        this._bullet.GetComponent<BulletDestruction>().ConfigureBullet(this._damge, NetworkPlayer.NetworkPlayerInstance.GetNickName());
        this._currentBulletCount = this._maxBulletCount;

        UpdateBulletInfo();
    }

    private void LoadSettings()
    {
        this._damge += (int)PlayerStats.Instance.GetDamageForce();
        this._bullet = PlayerStats.Instance.GetBullet();
        this._bulletHipCount = (int)(this._maxHipCount + PlayerStats.Instance.GetBulletPercent());
    }

    public void ReloadWeapon()
    {
        this._animSrc?.PlayReloadAnimation();

        this._bulletHipCount += this._currentBulletCount;
        this._currentBulletCount = Mathf.Clamp(this._bulletHipCount, 0, this._maxBulletCount);

        this._bulletHipCount -= this._currentBulletCount;

        UpdateBulletInfo();

        this._bullet.GetComponent<BulletDestruction>().ConfigureBullet(this._damge, NetworkPlayer.NetworkPlayerInstance.GetNickName());
        PlayerUI.Instance.ChangeReloadIndicatorState(false);
    }

    private void CallSingleShoot()
    {
        SingleShoot();
    }

    private bool SingleShoot()
    {
        if (this._currentBulletCount <= 0)
        {
            PlayerUI.Instance.ChangeReloadIndicatorState(true);
            return false;
        }

        this._animSrc?.PlayShootAnimation();

        bool isHit = false;
        RaycastHit target = ThrowRay(out isHit);
        this._currentBulletCount-=1;
        PlayerUI.Instance.DisplayBullets(this._currentBulletCount);

        SpawnBullet(target.point, isHit);

        if (this._recoilSrc == null)
            this._recoilSrc = this.GetComponent<Recoil>();
        this._recoilSrc.TakeRecoil();

        return true;
    }

    private RaycastHit ThrowRay(out bool isHit)
    {
        isHit = false;
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0.5f); // center of the screen
        float rayLength = this._shootDistance;

        // actual Ray
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength,this._bulletMask))
        {
            isHit = true;
            this._rfTarget.transform.position = hit.transform.position;
            this._gun.Shoot();
        }
        return hit;
    }

    private IEnumerator ShootWithDelay()
    {
        if (this._currentBulletCount > 0 && this._isShooting)
        {
            this._shooted = SingleShoot();

            yield return new WaitForSeconds(this._shotDelay);
            if (this._isShooting)
            {
                StartCoroutine(ShootWithDelay());
            }
        }
        if(this._currentBulletCount <= 0)
        {
            PlayerUI.Instance.ChangeReloadIndicatorState(true);
            this._isShooting = false;
        }
    }

    public void RemoveBullet(GameObject bullet)
    {
        GameObject.Destroy(bullet);
    }

    public void AddAmmo(int ammoCount)
    {
        if (this._bulletHipCount + ammoCount <= this._maxHipCount)
            this._bulletHipCount += ammoCount;

        PlayerUI.Instance.DisplayMaxBullets(this._bulletHipCount);
    }

    public void UpdateBulletInfo()
    {
        PlayerUI.Instance.DisplayBullets(this._currentBulletCount);
        PlayerUI.Instance.DisplayMaxBullets(this._bulletHipCount);
    }

    public void SpawnBullet(Vector3 lookVector, bool isHit)
    {
        GameObject projectile = Instantiate(this._bullet, this._startPosition.position, Quaternion.identity);

        if (isHit)
            projectile.transform.LookAt(lookVector);
        else
            projectile.transform.Rotate(Camera.main.transform.rotation.eulerAngles);

        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * this._speed);
    }
}

public enum WeaponType
{
    HandGun,
    Ar,
    Rifle
}
