using UnityEngine;

public class SingleDamageProjectile : ProjectileBase
{
    protected override void OnHit(Transform enemy)
    {
        EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
        if (enemyBase == null || enemyBase.EnemyHealth == null) return;

        if (enemyBase.EnemyHealth.Current <= 0) return;

        int damage = isCriticalHit ? Mathf.RoundToInt(config.damage * 1.5f) : config.damage;
        enemyBase.TakeDamage(damage, config.damageType);
    }
}