using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 3f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void ApplyDrag()
    {
        _rb.linearDamping = 6;
    }

    public void Move(Vector3 direction)
    {
        ApplyDrag();
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
}