using UnityEngine;

public class EnemyController : BaseMovement
{
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
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
}
