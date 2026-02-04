using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 10f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float groundDrag = 6f;
    public float airDrag = 0f;

    protected bool _isGrounded;
    protected bool _jumpHeld;

    protected Rigidbody _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    protected void ApplyDrag()
    {
        if (_isGrounded)
            _rb.linearDamping = groundDrag;
        else
            _rb.linearDamping = airDrag;
    }

    protected void Move(Vector3 moveDir)
    {
        _rb.MovePosition(
            _rb.position +
            moveDir * speed * Time.fixedDeltaTime
        );

        if (moveDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot =
                Quaternion.LookRotation(moveDir);

            Quaternion newRot =
                Quaternion.Slerp(
                    _rb.rotation,
                    targetRot,
                    rotationSpeed * Time.fixedDeltaTime
                );

            _rb.MoveRotation(newRot);
        }
    }

    protected void Jump()
    {
        if (!_isGrounded) return;

        _rb.linearVelocity = new Vector3(
            _rb.linearVelocity.x,
            0f,
            _rb.linearVelocity.z
        );

        _rb.AddForce(
            Vector3.up * jumpForce,
            ForceMode.Impulse
        );

        _isGrounded = false;
    }

    protected void ApplyBetterGravity()
    {
        if (_rb.linearVelocity.y < 0f)
        {
            _rb.AddForce(
                Vector3.up * Physics.gravity.y *
                (fallMultiplier - 1f),
                ForceMode.Acceleration
            );
        }
        else if (_rb.linearVelocity.y > 0f && !_jumpHeld)
        {
            _rb.AddForce(
                Vector3.up * Physics.gravity.y *
                (lowJumpMultiplier - 1f),
                ForceMode.Acceleration
            );
        }
    }
}
