using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    private List<GameObject> activeEnemies = new List<GameObject>();
    public int ActiveEnemyCount => activeEnemies.Count;

    public void SpawnWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawn.position, spawn.rotation);

            activeEnemies.Add(enemy);

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            health.OnEnemyDeath += () => activeEnemies.Remove(enemy);
        }
    }
}
