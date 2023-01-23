using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _animSrc;

    [SerializeField]
    private CharacterController _controller;

    private float _speed;

    private void Start()
    {
        this._controller.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerKeys.Instance.Horizontal !=0 || PlayerKeys.Instance.Vertical != 0)
        {
            this._animSrc.SetBool("isMovement", true);
            this._animSrc.SetFloat("Speed", this._controller.velocity.magnitude);
        }
        else
        {
            this._animSrc.SetBool("isMovement", false);
        }
    }

    public void SetWeaponBool(WeaponType type)
    {
        DisableAllWeaponAnim();
        this._animSrc.SetBool(type.ToString(), true);
    }

    public void SetDamageTrigger()
    {
        this._animSrc.SetTrigger("TakeDamage");
    }

    public void SetShotTrigger()
    {
        this._animSrc.SetTrigger("Shoot");
    }

    public void SetReloadTrigger()
    {
        this._animSrc.SetTrigger("Reload");
    }

    private void DisableAllWeaponAnim()
    {
        this._animSrc.SetBool(WeaponType.Ar.ToString(),false);
        this._animSrc.SetBool(WeaponType.HandGun.ToString(), false);
        this._animSrc.SetBool(WeaponType.Rifle.ToString(), false);
    }
}
