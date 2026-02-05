using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public float spawnRadius = 5f;
    public int enemies = 10;

    void Start()
    {
        for (int i = 0; i < enemies; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 point = GetRandomPoint(
            transform.position.x,
            transform.position.z,
            spawnRadius
        );

        Vector3 spawnPos = new Vector3(point.x, transform.position.y + 1, point.y);

        GameObject enemy = Instantiate(
            EnemyPrefab,
            spawnPos,
            Quaternion.identity
        );
    }

    private Vector2 GetRandomPoint(float x0, float y0, float radius)
    {
        float theta = Random.value * 2f * Mathf.PI;
        float r = Mathf.Sqrt(Random.value) * radius;

        float x = x0 + r * Mathf.Cos(theta);
        float y = y0 + r * Mathf.Sin(theta);

        return new Vector2(x, y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}