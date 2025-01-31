using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveConfig", menuName = "Configs/WaveConfig")]
public class WaveConfig : ScriptableObject
{
    public List<EnemyConfig> enemyTypes; // Типи ворогів у хвилі.
    public List<int> enemyCounts; // Кількість кожного типу ворогів.
    public float spawnDelay; // Затримка між спавнами.
    public PathConfig path; // Шлях, яким рухаються вороги.
}
