using UnityEngine;

public class SingleDamageProjectile : ProjectileBase
{
    protected override void OnHit(Transform enemy)
    {
        EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
        if (enemyBase == null) return;

        IEnemyHealth enemyHealth = enemyBase.EnemyHealth;
        if (enemyHealth == null) return;

        int damage = isCriticalHit ? Mathf.RoundToInt(config.damage * 1.5f) : config.damage;
        enemyBase.TakeDamage(damage, config.damageType);
    }
}