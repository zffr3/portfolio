using UnityEngine;

public class DroppedWeapon : MonoBehaviour
{
    private bool _isTaked;

    [SerializeField]
    private string _weapon;

    private void OnTriggerEnter(Collider other)
    {
        NetworkCharacter charSrc = other.GetComponent<NetworkCharacter>();
        if (charSrc != null )
        {
            PlayerUI.Instance.ChangeCrateUIActiveState(true);
            GiveWeaponAndDestroy(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        NetworkCharacter charSrc = other.GetComponent<NetworkCharacter>();
        if (charSrc != null)
                GiveWeaponAndDestroy(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NetworkCharacter>() != null )
                PlayerUI.Instance.ChangeCrateUIActiveState(false);
    }

    private void GiveWeaponAndDestroy(Collider collision)
    {
        if (PlayerKeys.Instance.TakeWeapon && !this._isTaked)
        {
            PlayerUI.Instance.ChangeCrateUIActiveState(false);
            this._isTaked = true;
            collision.gameObject.GetComponent<PlayerWeapon>().TakeWeaponToHand(this._weapon);
            DesctroyCrate();
        }
    }

    public void DesctroyCrate()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void SetDroppedWeapon(string newWeapon)
    {
        this._isTaked = false;
        this._weapon = newWeapon;

    }
}
