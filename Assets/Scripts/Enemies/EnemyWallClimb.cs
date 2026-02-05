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
    private EnemyMovement _movement;
    private Transform _transform;

    private bool _isClimbing;
    public bool IsClimbing => _isClimbing;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _movement = GetComponent<EnemyMovement>();
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
        {
            _isClimbing = true;
        }
        else
        {
            _isClimbing = false;
        }

        Debug.DrawRay(
            origin,
            forwardDir * checkDistance,
            _isClimbing ? Color.red : Color.green
        );
    }

    private void Climb()
    {
        _rb.linearDamping = 0;

        _transform.position +=
            Vector3.up *
            climbSpeed *
            Time.fixedDeltaTime;
    }
}
