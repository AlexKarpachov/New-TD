using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private List<WaveConfig> waves; // Налаштування хвиль
    [SerializeField] private Transform spawnPoint1; // Точка спавну для Route1
    [SerializeField] private Transform spawnPoint2; // Точка спавну для Route2
    [SerializeField] private float timeBetweenWaves = 5f;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
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
        GameObject enemyObj = ObjectPoolManager.Instance.GetObject(enemyTag);

        if (enemyObj != null)
        {
            Transform spawnPoint = useSecondSpawnPoint ? spawnPoint2 : spawnPoint1;
            Waypoints waypoints = config.route; // 🔹 Отримуємо маршрут із `EnemyConfig`

            if (waypoints == null)
            {
                Debug.LogError("Маршрут для " + config.enemyName + " не встановлений у EnemyConfig!");
                return;
            }

            enemyObj.transform.position = spawnPoint.position;
            enemyObj.GetComponent<EnemyBase>().Initialize(config, enemyObj.transform, waypoints.points); 
        }
    }
}
