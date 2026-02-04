using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseMovement
{
    private PlayerInputAction _input;
    private InputAction _moveAction;

    protected override void Awake()
    {
        base.Awake();

        _input = new PlayerInputAction();
        _moveAction = _input.Player.Move;
    }

    void OnEnable() => _input.Enable();
    void OnDisable() => _input.Disable();

    void FixedUpdate()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();

        Vector3 moveDir = new Vector3(
            input.x,
            0f,
            input.y
        );

        Move(moveDir);
    }
}
