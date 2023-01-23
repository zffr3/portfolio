using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _sprintForce;

    private float _currentSpeed;

    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _gravityScale;

    [SerializeField]
    private float _jumpForce;

    private float _velocity;

    [SerializeField]
    private CharacterController _controller;

    private Vector3 _movement;

    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    private float _jumpTimeout = 0.1f;
    private float _fallTimeout = 0.15f;

    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    private bool _grounded;
    private float _groundedOffset = -0.14f;
    private float _groundedRadius = 0.5f;
    private LayerMask _groundLayers;

    private bool _isJump;

    // Start is called before the first frame update
    void Start()
    {
        this._controller = this.GetComponent<CharacterController>();

        PlayerKeys.Instance.JumpBtnPressed += Jump;
        PlayerStats.Instance.RankUp += UpdateDataAfterRankUp;
    }

    private void OnDestroy()
    {
        PlayerKeys.Instance.JumpBtnPressed -= Jump;
        PlayerStats.Instance.RankUp -= UpdateDataAfterRankUp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GravityWithJump();
    }

    private void Move()
    {
        if (PlayerKeys.Instance.Shifted)
            this._currentSpeed = this._sprintForce;
        else
            this._currentSpeed = this._speed;

        float deltaX = PlayerKeys.Instance.Horizontal * this._currentSpeed;
        float deltaZ = PlayerKeys.Instance.Vertical * this._currentSpeed;

        this._movement = new Vector3(deltaX, 0f, deltaZ);
        this._movement = Vector3.ClampMagnitude(this._movement, this._currentSpeed);

        //this._velocity += this._gravity * this._gravityScale;

        //this._movement.y = this._velocity;

        this._movement.y = this._verticalVelocity;

        this._movement *= Time.deltaTime;
        this._movement = this.transform.TransformDirection(this._movement);


        this._controller.Move(this._movement);
    }

    private void Jump()
    {
        Debug.Log("Jump");
        this._isJump = true;
        //if (this._controller.isGrounded)
        //    this._velocity = Mathf.Sqrt(this._jumpForce * -2f * (this._gravity * this._gravityScale));
    }

    private void GravityWithJump()
    {
        if (this._grounded)
        {
            this._fallTimeoutDelta = this._fallTimeout;

            if (this._verticalVelocity < 0.0f)
                this._verticalVelocity = -2f;

            if (this._isJump && this._jumpTimeoutDelta <= 0.0f)
                this._verticalVelocity = Mathf.Sqrt(this._jumpForce * -2f * this._gravity);

            if (this._jumpTimeoutDelta >= 0.0f)
                this._jumpTimeoutDelta -= Time.deltaTime;
        }
        else
        {
            this._jumpTimeoutDelta = this._jumpTimeout;

            if (this._fallTimeoutDelta >= 0.0f)
                this._fallTimeoutDelta -= Time.deltaTime;

            this._isJump = false;
        }

        if (this._verticalVelocity < this._terminalVelocity)
            this._verticalVelocity += this._gravity * Time.deltaTime;
    }

    private void UpdateDataAfterRankUp()
    {
        float[] playerData = PlayerStats.Instance.GetCharacterMovementData();
        if (playerData.Length == 3)
            ConfigurateDat(playerData[0],playerData[1],playerData[2]);
    }

    public void ConfigurateDat(float walkSpeed, float sprintForce, float jumpForce)
    {
        this._speed = walkSpeed;
        this._sprintForce = sprintForce;
        this._jumpForce = jumpForce;
    }

    private void GroundCheck()
    {
        Vector3 spherePosition = new Vector3(this.transform.position.x, this.transform.position.y - this._groundedOffset, this.transform.position.z);
        this._grounded = Physics.CheckSphere(spherePosition, this._groundedRadius,this._groundLayers,QueryTriggerInteraction.Ignore);
    }
}
