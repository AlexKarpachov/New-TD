using UnityEngine;
using static WaveConfig;

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
        EnemyBase baseEnemy = enemy.GetComponent<EnemyBase>();
        if (baseEnemy != null && baseEnemy.EnemyHealth != null)
        {
            if (baseEnemy.EnemyHealth.Current <= 0) return;

            int damage = isCriticalHit ? Mathf.RoundToInt(config.damage * 1.5f) : config.damage;
            baseEnemy.TakeDamage(damage, config.damageType);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (config == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.explosionRadius);
    }
}
