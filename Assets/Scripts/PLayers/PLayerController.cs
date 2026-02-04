using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float rotationSpeed = 10f;

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

        Vector3 moveDir = new Vector3(
            input.x,
            0f,
            input.y
        );

        _rb.MovePosition(
            _rb.position +
            moveDir * speed * Time.fixedDeltaTime
        );

        if (moveDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);

            Quaternion newRot = Quaternion.Slerp(
                _rb.rotation,
                targetRot,
                rotationSpeed * Time.fixedDeltaTime
            );

            _rb.MoveRotation(newRot);
        }
    }
}