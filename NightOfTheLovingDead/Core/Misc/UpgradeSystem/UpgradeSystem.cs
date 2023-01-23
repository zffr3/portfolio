using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem instance;

    private PlayerStats _statSrc;

    [SerializeField]
    private float _weaponUpgradePercent;
    [SerializeField]
    private float _weponStep;

    [SerializeField]
    private float _healthUpgradePercent;
    [SerializeField]
    private float _healthStep;

    [SerializeField]
    private float _powerUpgradePercent;
    [SerializeField]
    private float _powerStep;

    [SerializeField]
    private List<UpgradeSystemContainer> _upgradeContainers;

    private void Awake()
    {
        EventBus.SubscribeToEvent(EventType.UPGRADEDATA_LOADED, ReadSavedData);
        instance = this;
    }

    private void OnDisable()
    {
        if (this._upgradeContainers != null)
        {
            for (int i = 0; i < this._upgradeContainers.Count; i++)
            {
                this._upgradeContainers[i].UnsubscribeFromEvent();
            }
        }
    }

    public bool Upgrade(int upgradeCost, UpgradeType type, UpgradeStage stage)
    {
        if (this._statSrc.DiscrabXp(upgradeCost))
        {
            UpgradeSystemContainer container = this._upgradeContainers[((int)type)];

            if (container != null)
            {
                if (type != UpgradeType.Weapon)
                {
                    container.ScaleParamAndInvoke((int)stage);
                    return true;
                }
                else
                {
                    container.Invoke((int)stage);
                    return true;
                }

            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public void Initialize(PlayerStats instance)
    {
        this._statSrc = instance;

        this._upgradeContainers = new List<UpgradeSystemContainer>();

        this._upgradeContainers.Add(new UpgradeSystemContainer(new List<Action<float>>
        {
            this._statSrc.UpgradeDamage,
            this._statSrc.UpgradeBulletCount,
            this._statSrc.UpgradeBulletType
        },
        this._weaponUpgradePercent,
        this._weponStep));

        this._upgradeContainers.Add(new UpgradeSystemContainer(new List<Action<float>>
        {
            this._statSrc.UpgradeCharacterHealth
        },
        this._healthUpgradePercent,
        this._healthStep));

        this._upgradeContainers.Add(new UpgradeSystemContainer(new List<Action<float>>
        {
            this._statSrc.UpgradeCharacterPower
        },
        this._powerUpgradePercent,
        this._powerStep));
    }

    public SerializableUpgradeData BuildDataToSave()
    {
        SerializableUpgradeData upgradeData = new SerializableUpgradeData(this._weaponUpgradePercent, this._healthUpgradePercent, this._powerUpgradePercent);
        return upgradeData;
    }

    private void ReadSavedData(object sender, object param)
    {
        SerializableUpgradeData save = param as SerializableUpgradeData;
        this._weaponUpgradePercent = save.WeaponUpgradePercent;
        this._healthUpgradePercent = save.HealthUpgradePercent;
        this._powerUpgradePercent = save.PowerUpgradePercent;
    }

    private class UpgradeSystemContainer
    {
        public List<Action<float>> UpgradeMethodPointers { get; private set; }

        private float _callParam;
        private float _paramStep;

        public UpgradeSystemContainer(List<Action<float>> actions, float param, float step)
        {
            this.UpgradeMethodPointers = actions;
            this._callParam = param;
            this._paramStep = step;

            EventBus.SubscribeToEvent(EventType.PLAYER_RANK_UPPED, UpdateCallParam);
        }

        public void UnsubscribeFromEvent()
        {
            EventBus.UnsubscribeFromEvent(EventType.PLAYER_RANK_UPPED, UpdateCallParam);
        }

        public void ScaleParamAndInvoke(int callIndex)
        {
            this.UpgradeMethodPointers[Mathf.Clamp(callIndex, 0, UpgradeMethodPointers.Count-1)].Invoke(this._callParam * (callIndex + 1));
        }

        public void Invoke(int callIndex)
        {
            this.UpgradeMethodPointers[Mathf.Clamp(callIndex, 0, UpgradeMethodPointers.Count-1)].Invoke(this._callParam);
        }

        private void UpdateCallParam(object sender, object param)
        {
            this._callParam += this._callParam * this._paramStep;
        }
    }
}
