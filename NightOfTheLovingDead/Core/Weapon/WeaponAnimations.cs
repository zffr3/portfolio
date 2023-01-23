using UnityEngine;

public class WeaponAnimations : MonoBehaviour
{
    [SerializeField]
    private Animator _animSrc;

    public void PlayShootAnimation()
    {
        this._animSrc.SetTrigger("Shoot");
    }

    public void PlayReloadAnimation()
    {
        this._animSrc.SetTrigger("Reload");
    }
}
