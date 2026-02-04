using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float spawnRadius = 10f;
    public float spawnDelay = 2f;

    private List<GameObject> _enemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
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

        _enemies.Add(enemy);
    }

    private Vector2 GetRandomPoint(float x0, float y0, float radius)
    {
        float theta = Random.value * 2f * Mathf.PI;
        float r = Mathf.Sqrt(Random.value) * radius;

        float x = x0 + r * Mathf.Cos(theta);
        float y = y0 + r * Mathf.Sin(theta);

        return new Vector2(x, y);
    }
}