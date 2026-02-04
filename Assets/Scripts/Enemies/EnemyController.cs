using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 10f;
    private GameObject _player;
    private Rigidbody _rb;


    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector3 vector_to_player = GetVectorToPlayer();
        Move(vector_to_player);
    }

    void Move(Vector3 moveDir)
    {
        // Move
        _rb.MovePosition(
            _rb.position +
            moveDir * speed * Time.fixedDeltaTime
        );

        // Rotate
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

    Vector3 GetVectorToPlayer()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 enemyPos = transform.position;

        Vector3 dir = new Vector3(
            playerPos.x - enemyPos.x,
            0,
            playerPos.z - enemyPos.z
        );

        return dir.normalized;
    }
}