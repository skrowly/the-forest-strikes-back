using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave 1 Animals")]
    public GameObject animal1Prefab;
    public GameObject animal2Prefab;

    [Header("Wave 2 Animals")]
    public GameObject animal3Prefab;
    public GameObject animal4Prefab;

    [Header("Wave 3 Animals")]
    public GameObject animal5Prefab;
    public GameObject animal6Prefab;

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;
    public float timeBetweenSpawns = 2.5f;

    private List<GameObject> activeEnemies = new List<GameObject>();
    public int ActiveEnemyCount => activeEnemies.Count;

    public void SpawnLevel(int level)
    {
        if (level == 1)
        {
            StartCoroutine(SpawnRoutine(new GameObject[] {
                animal1Prefab, animal1Prefab, animal1Prefab,
                animal2Prefab, animal2Prefab
            }));
        }
        else if (level == 2)
        {
            StartCoroutine(SpawnRoutine(new GameObject[] {
                animal1Prefab, animal1Prefab, animal1Prefab, animal1Prefab,
                animal2Prefab, animal2Prefab, animal2Prefab,
                animal3Prefab, animal3Prefab,
                animal4Prefab
            }));
        }
        else if (level == 3)
        {
            StartCoroutine(SpawnRoutine(new GameObject[] {
                animal1Prefab, animal1Prefab, animal1Prefab,
                animal2Prefab, animal2Prefab, animal2Prefab,
                animal3Prefab, animal3Prefab, animal3Prefab,
                animal4Prefab, animal4Prefab,
                animal5Prefab, animal5Prefab,
                animal6Prefab
            }));
        }
    }

    IEnumerator SpawnRoutine(GameObject[] enemies)
    {
        foreach (GameObject prefab in enemies)
        {
            if (prefab != null)
            {
                SpawnEnemy(prefab);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }

    void SpawnEnemy(GameObject prefab)
    {
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(prefab, spawn.position, spawn.rotation);
        activeEnemies.Add(enemy);
        Debug.Log("Spawned: " + prefab.name + " total active: " + activeEnemies.Count);

        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.OnEnemyDeath += () => activeEnemies.Remove(enemy);
        }
    }
}