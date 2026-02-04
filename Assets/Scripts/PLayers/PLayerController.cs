using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 6f;

    private Rigidbody _rb;
    private PlayerInputAction _input;
    private InputAction _moveAction;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _input = new PlayerInputAction();
        _moveAction = _input.Player.Move;
    }

    void OnEnable()
    {
        _input.Enable();
    }

    void OnDisable()
    {
        _input.Disable();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();

        Vector3 dir = new Vector3(
            input.x,
            0f,
            input.y
        );

        _rb.linearVelocity = dir * speed;
    }
}