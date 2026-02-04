using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    protected Rigidbody _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected void Move(Vector3 moveDir)
    {
        // Move
        _rb.MovePosition(
            _rb.position +
            moveDir * speed * Time.fixedDeltaTime
        );

        // Rotate
        if (moveDir.sqrMagnitude > 0.0001f)
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
