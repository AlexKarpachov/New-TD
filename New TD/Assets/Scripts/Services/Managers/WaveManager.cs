using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private List<WaveConfig> waves;
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private Waypoints route1;
    [SerializeField] private Waypoints route2;
    [SerializeField] private float startDelayTime = 5f;
    [SerializeField] private float timeBetweenWaves = 5f;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;
    private bool allWavesCompleted = false;
    public bool AllWavesCompleted => allWavesCompleted;

    private WaveCountdownUI countdown;

    // ✅ Доступ до кількості хвиль
    public int TotalWaves => waves.Count;

    // ✅ Поточна хвиля (нумерація з 1, не з 0)
    public int CurrentWave => Mathf.Clamp(currentWaveIndex + (isSpawning ? 1 : 0), 1, TotalWaves);

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        countdown = FindObjectOfType<WaveCountdownUI>();

        if (countdown != null)
        {
            countdown.StartCountdown(startDelayTime, () =>
            {
                StartCoroutine(SpawnWaves());
            });
        }
        else
        {
            StartCoroutine(SpawnWaves());
        }
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

            if (currentWaveIndex < waves.Count && countdown != null)
            {
                bool waitFinished = false;
                countdown.StartCountdown(timeBetweenWaves, () => waitFinished = true);
                yield return new WaitUntil(() => waitFinished);
            }
        }

        allWavesCompleted = true;
    }

    private IEnumerator SpawnWave(WaveConfig wave)
    {
        int totalEnemiesInWave = 0;
        foreach (var enemy in wave.enemies)
        {
            totalEnemiesInWave += enemy.count;
        }

        int spawnedCount = 0;

        foreach (var enemy in wave.enemies)
        {
            for (int i = 0; i < enemy.count; i++)
            {
                SpawnEnemy(enemy.enemyPrefab, enemy.config, enemy.spawnFromSecondPoint);
                spawnedCount++;

                if (spawnedCount < totalEnemiesInWave)
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
}
