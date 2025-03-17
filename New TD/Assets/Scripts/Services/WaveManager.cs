using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages enemy waves, controlling spawn points, intervals, and wave progression.
/// </summary>
public class WaveManager : MonoBehaviour, IWaveManager
{
    public static WaveManager Instance;

    [SerializeField] List<WaveConfig> waves; 
    [SerializeField] Transform spawnPoint1;
    [SerializeField] Transform spawnPoint2;
    [SerializeField] Waypoints route1;
    [SerializeField] Waypoints route2;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] int waveToActivateSecondSpawn = 5;

    int currentWaveIndex = 0;
    bool isSpawning = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitUntil(() => ObjectPool.Instance != null);

        while (currentWaveIndex < waves.Count)
        {
            isSpawning = true;
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            isSpawning = false;
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWaveIndex++;
        }
    }

    private IEnumerator SpawnWave(WaveConfig wave)
    {
        foreach (WaveConfig.EnemyWave enemyWave in wave.enemies)
        {
            for (int i = 0; i < enemyWave.count; i++)
            {
                SpawnEnemy(enemyWave.enemyPrefab, enemyWave.config, enemyWave.spawnFromSecondPoint);
                yield return new WaitForSeconds(wave.spawnDelay);
            }
        }
    }

    private void SpawnEnemy(string enemyTag, EnemyConfig config, bool useSecondSpawnPoint)
    {
        if (ObjectPool.Instance == null)
        {
            return;
        }

        useSecondSpawnPoint = currentWaveIndex + 1 >= waveToActivateSecondSpawn ? useSecondSpawnPoint : false;

        GameObject enemyObj = ObjectPool.Instance.GetObject(enemyTag, useSecondSpawnPoint ? spawnPoint2.position : spawnPoint1.position, Quaternion.identity);

        if (enemyObj != null)
        {
            Waypoints waypoints = useSecondSpawnPoint ? route2 : route1;

            if (waypoints == null)
            {

                return;
            }

            Debug.Log($"Spawning {enemyTag} at {(useSecondSpawnPoint ? "Spawn Point 2" : "Spawn Point 1")}, Route: {(useSecondSpawnPoint ? "Route 2" : "Route 1")}");
            enemyObj.GetComponent<EnemyBase>().Initialize(config, enemyObj.transform, waypoints.points);
        }
    }

    public void StartNextWave()
    {
        if (!isSpawning && currentWaveIndex < waves.Count)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++;
        }
    }

    public int GetCurrentWaveIndex()
    {
        return currentWaveIndex;
    }
}
