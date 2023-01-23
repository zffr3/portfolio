using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    const float GRAVITY = -9.81f;

    [SerializeField]
    private CharacterController _controller;

    private Vector2 _horizontalInput;

    private float _speed;

    [SerializeField]
    private float _normalSpeed;
    [SerializeField]
    private float _sprintSpeed;

    private Vector3 _velocity;

    private bool _sprinted;
    [SerializeField]
    private int _stamina;
    private int _staminaMax;

    private void Start()
    {
        this._staminaMax = 1000;
        this._stamina = this._staminaMax;

        //EventBus.SubscribeToEvent(EventType.RELEASE_PLAYER, RestStamina);
        EventBus.SubscribeToEvent(EventType.DEMON_BURNED, DisableComponent);

        StartCoroutine(StaminaLoop());
    }

    private void OnDestroy()
    {
        //EventBus.UnsubscribeFromEvent(EventType.RELEASE_PLAYER, RestStamina);
        EventBus.UnsubscribeFromEvent(EventType.DEMON_BURNED, DisableComponent);
    }

    private void OnEnable()
    {
        RestStamina(null, null);
    }

    private void Update()
    {
        Move();
        Sprint();
    }

    private void Move()
    {
        Vector3 moveVector = this.transform.right * this._horizontalInput.x + this.transform.forward * this._horizontalInput.y;
        this._controller.Move(moveVector * this._speed * Time.deltaTime);

        this._velocity.y += GRAVITY * Time.deltaTime;
        this._controller.Move(this._velocity * Time.deltaTime);
    }

    private void Sprint()
    {
        if (this._sprinted && this._stamina != 0)
        {
            this._speed = this._sprintSpeed;
        }
        else
        {
            this._speed = this._normalSpeed;
        }
    }

    public void ReceiveInput(Vector2 inputVector, bool sprint)
    {
        this._horizontalInput = inputVector;
        this._sprinted = sprint;
    }

    private void RestStamina(object sender, object param)
    {
        if(GameUi.instance != null)
        {
            StopAllCoroutines();
            this._sprinted = false;
            this._stamina = this._staminaMax;
            GameUi.instance.SyncStamina();
            StartCoroutine(StaminaLoop());
        }
    }

    private IEnumerator StaminaLoop()
    {
        if (this._sprinted && this._stamina != 0)
        {
            this._stamina -= 100;
            GameUi.instance.ChangeStaminaValue(-100);
        }

        if (!this._sprinted && this._stamina != this._staminaMax)
        {
            this._stamina += 100;
            GameUi.instance.ChangeStaminaValue(100);
        }

        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(StaminaLoop());
    }

    private void DisableComponent(object sender, object param)
    {
        this.enabled = false;
    }
}

