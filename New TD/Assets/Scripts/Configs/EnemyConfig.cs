using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "Configs/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public string enemyName;
    public int health;
    public int armor;
    public int mechanicalResistance;
    public int magicalResistance;
    public float speed;
    public int goldReward;
    public int scoreReward;
    public int livesDamage;
}
