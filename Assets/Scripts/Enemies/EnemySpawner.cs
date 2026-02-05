using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public Enemy Spawn()
    {
        Enemy enemy = Instantiate(
            _enemyPrefab,
            _transform.position,
            _transform.rotation
        );
        return enemy;
    }
}