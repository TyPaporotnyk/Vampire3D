using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;

    private GameObject _player;
    private Rigidbody _rb;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 dir = GetVectorToPlayer();
        Move(dir);
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

    private void Move(Vector3 moveDir)
    {
        _rb.MovePosition(
            _rb.position +
            moveDir * config.moveSpeed * Time.fixedDeltaTime
        );

        if (moveDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot =
                Quaternion.LookRotation(moveDir);

            Quaternion newRot =
                Quaternion.Slerp(
                    _rb.rotation,
                    targetRot,
                    config.rotationSpeed * Time.fixedDeltaTime
                );

            _rb.MoveRotation(newRot);
        }
    }
}
