using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyWallClimb : MonoBehaviour
{
    [SerializeField] private float checkDistance = 0.6f;
    [SerializeField] private float climbSpeed = 2f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float rayOffset = 0.25f;

    private Rigidbody _rb;
    private Transform _transform;

    private bool _isClimbing;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
    }

    public void MyFixedUpdate(Vector3 forwardDir)
    {
        CheckWall(forwardDir);

        if (_isClimbing)
            Climb();
    }

    private void CheckWall(Vector3 forwardDir)
    {
        RaycastHit hit;

        Vector3 origin =
            groundCheck.position +
            Vector3.up * rayOffset;

        if (Physics.Raycast(
            origin,
            forwardDir,
            out hit,
            checkDistance,
            wallLayer
        ))
            _isClimbing = true;
        else
            _isClimbing = false;
    }

    private void Climb()
    {
        Vector3 velocity = _rb.linearVelocity;

        velocity.y = climbSpeed;

        _rb.linearVelocity = velocity;
        Debug.Log(_transform.position);
    }
}
