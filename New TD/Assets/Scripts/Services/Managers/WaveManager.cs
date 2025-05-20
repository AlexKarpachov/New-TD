using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour, IWaveManager
{
    public static WaveManager Instance;

    [SerializeField] List<WaveConfig> waves;
    [SerializeField] Transform spawnPoint1;
    [SerializeField] Transform spawnPoint2;
    [SerializeField] Waypoints route1;
    [SerializeField] Waypoints route2;
    [SerializeField] float timeBetweenWaves = 5f;

    public int TotalWaves => waves.Count;
    public int CurrentWave => currentWaveIndex + 1;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;
    private bool allWavesCompleted = false;
    public bool AllWavesCompleted => allWavesCompleted;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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

            currentWaveIndex++;

            if (currentWaveIndex < waves.Count)
                yield return new WaitForSeconds(timeBetweenWaves);
        }

        allWavesCompleted = true;
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

    private void SpawnEnemy(string enemyTag, EnemyConfig config, bool spawnFromSecondPoint)
    {
        if (ObjectPool.Instance == null) return;

        Transform spawnPoint = spawnFromSecondPoint ? spawnPoint2 : spawnPoint1;
        Waypoints route = spawnFromSecondPoint ? route2 : route1;

        if (route == null) return;

        GameObject enemyObj = ObjectPool.Instance.GetObject(enemyTag, spawnPoint.position, Quaternion.identity);

        if (enemyObj != null)
        {
            var enemy = enemyObj.GetComponent<EnemyBase>();
            enemy.Initialize(config, enemyObj.transform, route.points);
            enemy.ResetEnemy();
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
