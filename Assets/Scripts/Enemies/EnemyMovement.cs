using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance = 0.25f;
    public float groundDrag = 6f;
    public float airDrag = 0f;

    private Rigidbody _rb;
    private bool _isGrounded;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void MyFixedUpdate()
    {
        CheckGround();
        ApplyDrag();
    }

    private void ApplyDrag()
    {
        if (_isGrounded)
            _rb.linearDamping = groundDrag;
        else
            _rb.linearDamping = airDrag;
    }

    public void Move(Vector3 direction)
    {
        _rb.MovePosition(
            _rb.position +
            direction * moveSpeed * Time.fixedDeltaTime
        );

        if (direction.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot =
                Quaternion.LookRotation(direction);

            Quaternion newRot =
                Quaternion.Slerp(
                    _rb.rotation,
                    targetRot,
                    rotationSpeed * Time.fixedDeltaTime
                );

            _rb.MoveRotation(newRot);
        }
    }
    private void CheckGround()
    {
        _isGrounded = Physics.Raycast(
            groundCheck.position,
            Vector3.down,
            groundDistance,
            groundLayer
        );
    }
}