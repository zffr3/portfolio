using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagble
{
    [SerializeField]
    private float _maxHealth;

    [SerializeField]
    private float _healthCount;

    private void Start()
    {
        EventBus.SubscribeToEvent(EventType.HEALTH_UPGRADED, UpdateHelath);
    }

    private void OnDisable()
    {
        EventBus.UnsubscribeFromEvent(EventType.HEALTH_UPGRADED, UpdateHelath);
    }

    public void AddHealth(float healthCount)
    {
        this._healthCount = Mathf.Clamp(this._healthCount + healthCount,0,this._maxHealth);
        PlayerUI.Instance.DisplayHealth(this._healthCount);
    }

    public void TakeDamage(float damage, string plrName)
    {
        if (plrName != NetworkPlayer.NetworkPlayerInstance.GetNickName())
            TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        this._healthCount -= damage;
        PlayerUI.Instance.DisplayHealth(this._healthCount);
        PlayerUI.Instance.ChangeAlphaBloodScreen(damage, this._healthCount);

        if (this._healthCount <= 0)
            NetworkPlayer.NetworkPlayerInstance.Die();
    }

    private void UpdateHelath(object sender, object param)
    {
        this._maxHealth =  (float)param;
        AddHealth(this._maxHealth - this._healthCount);
    }

    public void Initialize(float health)
    {
        this._maxHealth = health;
        this._healthCount = this._maxHealth;
        PlayerUI.Instance.DisplayHealth(this._healthCount);
    }
}
