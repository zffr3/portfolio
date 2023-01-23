using UnityEngine;

public class WalkingZombieController : MonoBehaviour
{
    public static WalkingZombieController Instance;

    [SerializeField]
    private float _stepValue;

    [SerializeField]
    private float _zombieDamage;
    [SerializeField]
    private float _zombieSpeed;
    [SerializeField]
    private float _zombieHealth;

    [SerializeField]
    private int _zombieDeathReward;

    [SerializeField]
    private float _zombieSensRadius;

    private void Awake()
    {
        Instance = this;
    }

    public void MultiplyValue()
    {
        this._zombieDamage += this._stepValue;
        this._zombieSpeed += this._stepValue;
        this._zombieHealth += this._stepValue * 10;

        this._zombieDeathReward += (int)(this._stepValue * 10);

        this._zombieSensRadius += this._stepValue * 10;
    }

    public float[] GetZombieStatValues()
    {
        return new float[] { this._zombieDamage, this._zombieSpeed, this._zombieHealth, this._zombieDeathReward, this._zombieSensRadius};
    }
}