using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveConfig", menuName = "Configs/WaveConfig")]
public class WaveConfig : ScriptableObject
{
    public float spawnDelay = 1f; // Delay between spawns

    [System.Serializable]
    public class EnemyWave
    {
        public string enemyPrefab;
        public EnemyConfig config; 
        public int count; // Number of enemies
        public bool spawnFromSecondPoint; // If true, spawn from point 2
    }

    public List<EnemyWave> enemies;
}
