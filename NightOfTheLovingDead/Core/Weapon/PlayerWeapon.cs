using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public static PlayerWeapon Instance { get; private set; }

    [SerializeField]
    private List<WeaponData> _mineWeapons;

    [SerializeField]
    private WeaponData[] _weaponsInHand;

    [SerializeField]
    private GameObject _showedWeapon;

    private int _showedWeapInd;

    [SerializeField]
    private int _selectedWeapon;

    [SerializeField]
    private PlayerAnimation _animSrc;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        this._animSrc = this.GetComponent<PlayerAnimation>();
        this._selectedWeapon = 0;

        string[] savedWeapons = PlayerStats.Instance.GetWeapons();
        if (savedWeapons != null)
        {
            for (int i = 0; i < savedWeapons.Length; i++)
            {
                if (savedWeapons[i] != null)
                {
                    TakeWeaponToHand(savedWeapons[i]);
                }
            }
        }
    }

    private void OnEnable()
    {
        PlayerKeys.Instance.NextItmPressed += MoveNext;
        PlayerKeys.Instance.PrevItmPressed += MovePrevious;
        PlayerKeys.Instance.ReloadBtnPressed += ReloadWeapon;
    }

    private void OnDisable()
    {
        PlayerKeys.Instance.NextItmPressed -= MoveNext;
        PlayerKeys.Instance.PrevItmPressed -= MovePrevious;
        PlayerKeys.Instance.ReloadBtnPressed -= ReloadWeapon;
    }

    private void ReloadWeapon()
    {
        if (this._weaponsInHand[this._selectedWeapon] != null)
        {
            this._weaponsInHand[this._selectedWeapon].ReloadWeapon();
            this._animSrc.SetReloadTrigger();
        }
    }

    public void TakeWeaponToHand(string weaponName)
    {
        if (weaponName == "NOT_FINDED")
        {
            return;
        }

        if (this._weaponsInHand[this._selectedWeapon] != null)
        {
            this._weaponsInHand[this._selectedWeapon].gameObject.SetActive(false);
        }


        int handIndex = -1;
        for (int i = 0; i < this._weaponsInHand.Length; i++)
        {
            if (this._weaponsInHand[i] == null)
            {
                handIndex = i;
                break;
            }
        }

        int newWeaponIndex = 0;
        for (int i = 0; i < this._mineWeapons.Count; i++)
        {
            if (this._mineWeapons[i].GetWeaponName() == weaponName)
            {
                newWeaponIndex = i;
                break;
            }
        }

        if (handIndex >= 0)
        {
            this._selectedWeapon = handIndex;
        }

        this._weaponsInHand[this._selectedWeapon] = this._mineWeapons[newWeaponIndex];
        this._weaponsInHand[this._selectedWeapon].gameObject.SetActive(true);

        this._mineWeapons[newWeaponIndex].gameObject.SetActive(true);
        this._mineWeapons[newWeaponIndex].SetWeaponClip();
        this._animSrc.SetWeaponBool(this._mineWeapons[newWeaponIndex].GetWeaponType());

        TakeWeaponInHand(newWeaponIndex);
    }

    private void MoveNext()
    {
        if (this._weaponsInHand != null && this._weaponsInHand.Length != 0 && this._weaponsInHand[this._selectedWeapon] != null)
        {
            int sIndexTemp = this._selectedWeapon;

            if (sIndexTemp == this._weaponsInHand.Length - 1)
                sIndexTemp = 0;
            else
                sIndexTemp++;

            if (this._weaponsInHand[sIndexTemp] == null)
                return;
            ChangeWeapon(sIndexTemp, this._weaponsInHand[sIndexTemp].GetWeaponName());
        }
    }

    private void MovePrevious()
    {
        if (this._weaponsInHand != null && this._weaponsInHand.Length != 0 && this._weaponsInHand[this._selectedWeapon] != null)
        {
            int sIndexTemp = this._selectedWeapon;

            if (sIndexTemp == 0)
                sIndexTemp = this._weaponsInHand.Length - 1;
            else
                sIndexTemp--;

            if (this._weaponsInHand[sIndexTemp] == null)
                return;
            ChangeWeapon(sIndexTemp, this._weaponsInHand[sIndexTemp].GetWeaponName());
        }
    }

    public void AddAmmoToWeapon(int ammoCount)
    {
        this._weaponsInHand[this._selectedWeapon].AddAmmo(ammoCount);
    }

    private void TakeWeaponInHand(int wIndex)
    {
        if (this._showedWeapon != null)
            this._showedWeapon.SetActive(false);

        this._mineWeapons[wIndex].gameObject.SetActive(true);
        this._showedWeapon = this._mineWeapons[wIndex].gameObject;
    }

    private void ChangeWeapon(int selectedWeapon, string wName)
    {
        this._weaponsInHand?[this._selectedWeapon].gameObject.SetActive(false);
        this._weaponsInHand?[selectedWeapon].gameObject.SetActive(true);

        this._animSrc.SetWeaponBool(this._mineWeapons[selectedWeapon].GetWeaponType());
        this._weaponsInHand[selectedWeapon].UpdateBulletInfo();

        this._selectedWeapon = selectedWeapon;
    }

    public string[] GetHands()
    {
        string[] playerHands = new string[this._weaponsInHand.Length];
        for (int i = 0; i < this._weaponsInHand.Length; i++)
        {
            if (this._weaponsInHand[i] != null)
            {
                playerHands[i] = this._weaponsInHand[i].GetWeaponName();
            }
            else
            {
                playerHands[i] = "NOT_FINDED";
            }

        }

        return playerHands;
    }
}
