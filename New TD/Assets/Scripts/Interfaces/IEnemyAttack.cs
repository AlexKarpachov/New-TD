using UnityEngine;

public interface IEnemyAttack
{
    void Initialize(EnemyConfig config, Transform enemyTransform);
    void Update();
}
