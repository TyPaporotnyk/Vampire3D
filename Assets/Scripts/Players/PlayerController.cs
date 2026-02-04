using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseMovement
{
    public Transform groundCheck;
    public float groundDistance = 0.25f;
    public LayerMask groundLayer;

    private PlayerInputAction _input;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    private Vector3 _moveDir;
    private bool _jumpPressed;

    protected override void Awake()
    {
        base.Awake();

        _input = new PlayerInputAction();
        _moveAction = _input.Player.Move;
        _jumpAction = _input.Player.Jump;
    }

    void OnEnable() => _input.Enable();
    void OnDisable() => _input.Disable();

    void Update()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();

        _moveDir = new Vector3(
            input.x,
            0f,
            input.y
        );

        if (_jumpAction.triggered)
            _jumpPressed = true;

        _jumpHeld = _jumpAction.IsPressed();
    }

    void FixedUpdate()
    {
        CheckGround();
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
            groundDistance,
            groundLayer
        );
    }

    void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundDistance
        );
    }
}
