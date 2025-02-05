using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewWaveConfig", menuName = "Configs/WaveConfig")]
public class WaveConfig : ScriptableObject
{
    public float spawnDelay = 1f;

    [System.Serializable]
    public class EnemyWave
    {
        public string enemyPrefab; // "enemy1", "enemy2"
        public EnemyConfig config;
        public int count;
        public bool spawnFromSecondPoint; // Якщо true - спавнить ворога з Route2
    }

    public List<EnemyWave> enemies;
}
