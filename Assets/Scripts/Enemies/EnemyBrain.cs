using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    private Transform _player;
    private EnemyMovement _movement;
    private EnemyWallClimb _climb;

    private void Awake()
    {
        _movement = GetComponent<EnemyMovement>();
        _climb = GetComponent<EnemyWallClimb>();
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null) _player = playerObj.transform;
    }

    public void MyFixedUpdate()
    {
        if (_player == null) return;

        Vector3 dir = GetDirectionToPlayer();

        _climb.MyFixedUpdate(dir);

        // if (!_climb.IsClimbing)
        _movement.Move(dir);
    }

    private Vector3 GetDirectionToPlayer()
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
