using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [SerializeField]
    private Ranks _currentRank;

    [SerializeField]
    private int _experience;

    [SerializeField]
    private List<GameObject> _bullets;

    [SerializeField]
    private float _walkSpeed;
    [SerializeField]
    private float _sprintForce;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _damageForce;
    [SerializeField]
    private float _healthCount;
    [SerializeField]
    private float _bulletPercent;
    [SerializeField]
    private int _bulletId;
    [SerializeField]
    private string[] _handWeapon;


    public event System.Action RankUp;

    private void Awake()
    {
        EventBus.SubscribeToEvent(EventType.PLAYERDATA_LOADED, ReadSavedData);
        Instance = this;

        this._currentRank = Ranks.Mizunoto;

        EventBus.Dispath(EventType.PLAYER_RANK_UPPED, this, this._currentRank);

        this.GetComponent<UpgradeSystem>().Initialize(this);
        AddXp(800);
    }


    private void Start()
    {
        EventBus.Dispath(EventType.PLAYER_INITIALIZED, this, this);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.PLAYERDATA_LOADED, ReadSavedData);
    }

    public void AddXp(int xp)
    {
        this._experience += xp;
        PlayerUI.Instance.UpdateXpText(this._experience.ToString());

        CheckRank();
    }

    public bool DiscrabXp(int cost)
    {
        if (this._experience - cost < 0)
            return false;

        this._experience -= cost;

        PlayerUI.Instance.UpdateXpText(this._experience.ToString());

        CheckRank();

        return true;
    }

    public float[] GetCharacterMovementData()
    {
        return new float[] { this._walkSpeed, this._sprintForce, this._jumpForce};
    }

    public float GetDamageForce()
    {
        return this._damageForce;
    }

    public float GetBulletPercent()
    {
        return this._bulletPercent;
    }

    public GameObject GetBullet()
    {
        return this._bullets[this._bulletId];
    }

    public float GetMaxPlayerHealth()
    {
        return this._healthCount;
    }

    private void CheckRank()
    {
        if (this._experience >= (int)this._currentRank*3)
            SetNewRank();
    }

    public Ranks GetCurrentRank()
    {
        return this._currentRank;
    }

    private void SetNewRank()
    {
        this._currentRank = (Ranks)((int)this._currentRank * 3);

        RankUp?.Invoke();
        EventBus.Dispath(EventType.PLAYER_RANK_UPPED, this, this._currentRank);
    }

    public void UpgradeCharacterPower(float powerPercent)
    {
        this._sprintForce += this._sprintForce * powerPercent;
        this._jumpForce += this._jumpForce * powerPercent;

        EventBus.Dispath(EventType.POWER_UPGRADED, this, new float[] { this._walkSpeed, this._sprintForce, this._jumpForce });
    }

    public void UpgradeCharacterHealth(float healthPercent)
    {
        this._healthCount += this._healthCount * healthPercent;
        EventBus.Dispath(EventType.HEALTH_UPGRADED, this, this._healthCount);
    }

    public void UpgradeDamage(float damagePercent)
    {
        this._damageForce += this._damageForce * damagePercent;

        EventBus.Dispath(EventType.WEAPON_UPGRADED, "Damage", this._damageForce);
    }

    public void UpgradeBulletCount(float countPercent)
    {
        this._bulletPercent = countPercent * 100;

        EventBus.Dispath(EventType.WEAPON_UPGRADED, "BulletCount", this._bulletPercent);
    }

    public string[] GetWeapons()
    {
        return this._handWeapon;
    }

    public void UpgradeBulletType(float param)
    {
        this._bulletId += 1;
        if (this._bulletId >= this._bullets.Count)
        {
            this._bulletId = this._bullets.Count;
        }

        EventBus.Dispath(EventType.WEAPON_UPGRADED, "BulletType", this._bullets[this._bulletId]);
    }

    public SerializablePlayerData BuildDataToSave()
    {
        SerializableVector4 position = new SerializableVector4(NetworkPlayer.NetworkPlayerInstance.GetCharacterTransform().position);
        SerializableVector4 rotation = new SerializableVector4(NetworkPlayer.NetworkPlayerInstance.GetCharacterTransform().rotation);

        SerializablePlayerData playerData = new SerializablePlayerData(position, rotation, PlayerWeapon.Instance.GetHands(), this._healthCount, this._experience, this._currentRank, this._walkSpeed, this._sprintForce, this._jumpForce, this._damageForce, this._bulletPercent, this._bulletId);

        return playerData;
    }

    private void ReadSavedData(object sender, object param)
    {
        SerializablePlayerData save = param as SerializablePlayerData;
        NetworkPlayer.NetworkPlayerInstance.SetCharacterTransform(save.PlayerPosition.GetVector3(), save.PlayerRotation.GetQuaternion());
        this._handWeapon = save.HandWeapons;

        this._healthCount = save.HealthCount;

        EventBus.Dispath(EventType.HEALTH_UPGRADED, this, this._healthCount);

        this._experience = save.XpCount;
        this._currentRank = save.PlayerRank;

        EventBus.Dispath(EventType.PLAYER_RANK_UPPED, this, this._currentRank);

        this._walkSpeed = save.WalkSpeed;
        this._sprintForce = save.SprintForce;
        this._jumpForce = save.JumpForce;

        EventBus.Dispath(EventType.POWER_UPGRADED, this, new float[] { this._walkSpeed, this._sprintForce, this._jumpForce });

        this._damageForce = save.DamageForce;
        this._bulletPercent = save.BulletPercent;
        this._bulletId = save.BulletId;

        EventBus.Dispath(EventType.WEAPON_UPGRADED, "Damage", this._damageForce);
        EventBus.Dispath(EventType.WEAPON_UPGRADED, "BulletCount", this._bulletPercent);
        EventBus.Dispath(EventType.WEAPON_UPGRADED, "BulletType", this._bullets[this._bulletId]);
    }
}

public enum Ranks
{
    Mizunoto = 333,
    Mizunoe = 999,
    Kanoto = 2997,
    Kanoe = 8991,
    Tsuchinoto = 26973,
    Tsuchinoe = 80919,
    Hinoto = 242757,
    Hinoe = 728271,
    Kinoto = 2184813,
    Kinoe = 6554439,
    Hashira = 19663317
}
