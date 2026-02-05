using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    private Transform _player;
    private EnemyMovement movement;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null) _player = playerObj.transform;
    }

    public void MyFixedUpdate()
    {
        if (_player == null) return;

        Vector3 dir = GetDirectionToPlayer();
        movement.Move(dir);
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
