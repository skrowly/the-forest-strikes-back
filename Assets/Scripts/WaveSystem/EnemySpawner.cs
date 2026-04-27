using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave 1 Animals")]
    public GameObject animal1Prefab;    // basic animal
    public GameObject animal2Prefab;    // slightly stronger

    [Header("Wave 2 Animals")]
    public GameObject animal3Prefab;    // jumper
    public GameObject animal4Prefab;    // shooter

    [Header("Wave 3 Animals")]
    public GameObject animal5Prefab;    // tank
    public GameObject animal6Prefab;    // fast aggressive

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;
    public float timeBetweenSpawns = 1.5f;  // delay between each spawn

    private List<GameObject> activeEnemies = new List<GameObject>();
    public int ActiveEnemyCount => activeEnemies.Count;

    public void SpawnLevel(int level)
    {
        if (level == 1)
        {
            StartCoroutine(SpawnRoutine(new GameObject[] {
                animal1Prefab, animal1Prefab, animal1Prefab,  // 3 basic
                animal2Prefab, animal2Prefab                   // 2 stronger
            }));
        }
        else if (level == 2)
        {
            StartCoroutine(SpawnRoutine(new GameObject[] {
                animal1Prefab, animal2Prefab,                  // returning animals
                animal3Prefab, animal3Prefab,                  // 2 jumpers
                animal4Prefab                                   // 1 shooter
            }));
        }
        else if (level == 3)
        {
            StartCoroutine(SpawnRoutine(new GameObject[] {
                animal3Prefab, animal4Prefab,                  // returning
                animal5Prefab, animal5Prefab,                  // 2 tanks
                animal6Prefab, animal6Prefab, animal6Prefab   // 3 fast
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

        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.OnEnemyDeath += () => activeEnemies.Remove(enemy);
        }
    }
}