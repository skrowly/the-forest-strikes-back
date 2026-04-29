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
    public float timeBetweenGroups = 5f;
    public int enemiesPerGroup = 2;

    private List<GameObject> activeEnemies = new List<GameObject>();
    public int ActiveEnemyCount => activeEnemies.Count;

    public void SpawnLevel(int level)
    {
        if (level == 1)
        {
            StartCoroutine(SpawnInGroups(new GameObject[] {
                animal1Prefab, animal1Prefab, animal1Prefab,
                animal2Prefab, animal2Prefab
            }));
        }
        else if (level == 2)
        {
            StartCoroutine(SpawnInGroups(new GameObject[] {
                animal1Prefab, animal1Prefab, animal1Prefab, animal1Prefab,
                animal2Prefab, animal2Prefab, animal2Prefab,
                animal3Prefab, animal3Prefab,
                animal4Prefab
            }));
        }
        else if (level == 3)
        {
            StartCoroutine(SpawnInGroups(new GameObject[] {
                animal1Prefab, animal1Prefab, animal1Prefab,
                animal2Prefab, animal2Prefab, animal2Prefab,
                animal3Prefab, animal3Prefab, animal3Prefab,
                animal4Prefab, animal4Prefab,
                animal5Prefab, animal5Prefab,
                animal6Prefab
            }));
        }
    }

    IEnumerator SpawnInGroups(GameObject[] enemies)
    {
        int index = 0;

        while (index < enemies.Length)
        {
            // spawn enemiesPerGroup at once
            for (int i = 0; i < enemiesPerGroup && index < enemies.Length; i++)
            {
                if (enemies[index] != null)
                {
                    SpawnEnemy(enemies[index]);
                }
                index++;
            }

            Debug.Log("Spawned group, total active: " + activeEnemies.Count);

            //wait before next group
            yield return new WaitForSeconds(timeBetweenGroups);
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