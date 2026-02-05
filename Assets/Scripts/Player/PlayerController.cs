using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;
    [SerializeField] private ParticleSystem jumpFx;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private PlayerInputAction _input;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _pauseAction;
    private GameObject _camera;
    private PlayerAnimator _animator;

    private Vector3 _moveDir;
    private bool _jumpPressed;
    private bool _jumpHeld;
    private bool _isGrounded;

    private Rigidbody _rb;

    void Awake()
    {
        _camera = GameObject.FindWithTag("MainCamera");
        _input = new PlayerInputAction();
        _moveAction = _input.Player.Move;
        _jumpAction = _input.Player.Jump;
        _pauseAction = _input.Player.DebugPause;

        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<PlayerAnimator>();
    }

    void OnEnable() => _input.Enable();
    void OnDisable() => _input.Disable();

    void Update()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();

        Vector3 camForward = _camera.transform.forward;
        Vector3 camRight = _camera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        _moveDir =
            camForward * input.y +
            camRight * input.x;

        if (_jumpAction.triggered)
            _jumpPressed = true;

        _jumpHeld = _jumpAction.IsPressed();

        if (_pauseAction.triggered)
        {
            Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
        }
    }

    void FixedUpdate()
    {
        CheckGround();

        _animator.SetAnimation("IsGrounded", _isGrounded);

        ApplyDrag();
        Move(_moveDir);

        if (_jumpPressed)
        {
            Jump();
            _jumpPressed = false;
        }

        ApplyBetterGravity();
    }

    void CheckGround()
    {
        _isGrounded = Physics.Raycast(
            groundCheck.position,
            Vector3.down,
            config.groundDistance,
            groundLayer
        );
    }


    private void ApplyDrag()
    {
        if (_isGrounded)
            _rb.linearDamping = config.groundDrag;
        else
            _rb.linearDamping = config.airDrag;
    }

    protected void Move(Vector3 moveDir)
    {
        _animator.StartAnimation("");
        _rb.MovePosition(
            _rb.position +
            moveDir * config.moveSpeed * Time.fixedDeltaTime
        );

        if (moveDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot =
                Quaternion.LookRotation(moveDir);

            Quaternion newRot =
                Quaternion.Slerp(
                    _rb.rotation,
                    targetRot,
                    config.rotationSpeed * Time.fixedDeltaTime
                );

            _rb.MoveRotation(newRot);
        }
    }

    protected void Jump()
    {
        if (!_isGrounded)
        {
            _animator.StopAnimation("IsAirborne");
            return;
        }

        _rb.linearVelocity = new Vector3(
            _rb.linearVelocity.x,
            0f,
            _rb.linearVelocity.z
        );

        _rb.AddForce(
            Vector3.up * config.jumpSpeed,
            ForceMode.Impulse
        );
        Instantiate(
            jumpFx,
            groundCheck.position,
            Quaternion.Euler(-90, 0, 0)
        );

        _animator.StartAnimation("IsAirborne");

        _isGrounded = false;
    }

    protected void ApplyBetterGravity()
    {
        if (_rb.linearVelocity.y < 0f)
        {
            _rb.AddForce(
                Vector3.up * Physics.gravity.y *
                (config.fallMultiplier - 1f),
                ForceMode.Acceleration
            );
        }
        else if (_rb.linearVelocity.y > 0f && !_jumpHeld)
        {
            _rb.AddForce(
                Vector3.up * Physics.gravity.y *
                (config.lowJumpMultiplier - 1f),
                ForceMode.Acceleration
            );
        }
    }
}
