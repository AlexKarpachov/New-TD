using UnityEngine;

/// <summary>
/// Applies area damage to all enemies within explosion radius.
/// Used by artillery and dark mage towers.
/// </summary>
public class ExplosiveProjectile : ProjectileBase
{
    protected override void OnHit(Transform enemy)
    {
        // Apply damage to the main target
        ApplyDamageTo(enemy);

        // Find nearby enemies in explosion radius
        Collider[] colliders = Physics.OverlapSphere(enemy.position, config.explosionRadius);

        foreach (var collider in colliders)
        {
            if (collider.transform == enemy) continue; // already damaged
            EnemyBase nearbyEnemy = collider.GetComponent<EnemyBase>();
            if (nearbyEnemy != null)
            {
                ApplyDamageTo(nearbyEnemy.transform);
            }
        }
    }

    private void ApplyDamageTo(Transform enemy)
    {
        IEnemyHealth enemyHealth = enemy.GetComponent<EnemyBase>()?.EnemyHealth;
        if (enemyHealth != null)
        {
            int damage = isCriticalHit ? Mathf.RoundToInt(config.damage * 1.5f) : config.damage;
            enemyHealth.TakeDamage(damage, config.damageType);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (config == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.explosionRadius);
    }
}
