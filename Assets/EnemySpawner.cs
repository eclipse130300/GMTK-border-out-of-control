using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] private int minWaveSpawnAmount;
    [SerializeField] private int maxWaveSpawnAmount;

    [SerializeField] private int minSoloSpawnDelay;
    [SerializeField] private int maxSoloSpawnDelay;

    [SerializeField] private int waveDelay;

/*    [SerializeField] private int iD;
    [SerializeField] private float cdDuration;
    [SerializeField] private CoolDownSystem coolDownSystem;*/

    [SerializeField] Transform[] spawnPoints;

/*    public int CoolDownId => iD;
    public float CoolDownDuration => cdDuration;*/

    private int enemiesToSpawn;

    private void Start()
    {
        StartSpawn();
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnWave());
        StartCoroutine(SpawnEnemyLoop());
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            enemiesToSpawn = UnityEngine.Random.Range(minWaveSpawnAmount, maxWaveSpawnAmount + 1);

            var randomPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy(spawnPoints[randomPointIndex].position);
            }

            yield return new WaitForSeconds(waveDelay);
        }
    }

    private void SpawnEnemy(Vector3 pointToSpawn)
    {
        Instantiate(enemyPrefab, pointToSpawn, Quaternion.identity, gameObject.transform);
    }

    IEnumerator SpawnEnemyLoop()
    {
        while (true)
        {
            var randomPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            var randomSoloSpawnDelay = UnityEngine.Random.Range(minSoloSpawnDelay, maxSoloSpawnDelay);

            SpawnEnemy(spawnPoints[randomPointIndex].position);

            yield return new WaitForSeconds(randomSoloSpawnDelay);
        }
    }


}
