using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public float spawnRadius = 5f;
    public float spawnTrigerRadius = 10f;
    public float spawnDelay = 2f;

    private List<GameObject> _enemies = new List<GameObject>();
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        // StartCoroutine(SpawnLoop());
    }

    void Update()
    {
        bool isTrigered = CheckTrigger();
        Debug.Log(isTrigered);
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

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnTrigerRadius);
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

    private bool CheckTrigger()
    {
        Vector3 playerPos = _player.transform.position;
        Vector3 targetPos = transform.position;

        Vector3 diff = playerPos - targetPos;
        diff.y = 0f;

        return diff.sqrMagnitude <= spawnTrigerRadius * spawnTrigerRadius;
    }
}