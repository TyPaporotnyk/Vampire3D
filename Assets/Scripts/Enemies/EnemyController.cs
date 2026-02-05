using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float spawnRate = 1f;
    public int maxEnemies = 100;
    public int enemiesCount { get; private set; }
    private List<EnemySpawner> _spawners = new List<EnemySpawner>();
    private List<Enemy> _enemies = new List<Enemy>();

    private void Start()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            if (obj.TryGetComponent(out EnemySpawner spawner))
                _spawners.Add(spawner);
        }
        StartCoroutine(SpawnLoop());
    }

    void FixedUpdate()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].MyFixedUpdate();
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnFromAll();

            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnFromAll()
    {
        foreach (var spawner in _spawners)
        {
            if (enemiesCount < maxEnemies)
            {
                Enemy enemy = spawner.Spawn();
                _enemies.Add(enemy);
                enemiesCount += 1;
            }
        }
    }
}