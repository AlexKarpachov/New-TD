using UnityEngine;

/// <summary>
/// Handles tower attack logic.
/// </summary>
public class TowerAttack : ITowerAttack
{
    TowerConfig config;
    Transform towerTransform;
    Vector3 spawnOffset = new Vector3 (0, 2, 0);
    float lastAttackTime = 0f;
    float attackDelay;

    public TowerAttack(TowerConfig config, Transform towerTransform)
    {
        this.config = config;
        this.towerTransform = towerTransform;
        attackDelay = 1f / config.fireRate;
    }

    public void Attack(Transform enemy)
    {
        if (enemy == null) return;

        // 1f / 1 = 1 shot per second; 1f / 2 = 2 shots per second; 1f / 0.5 = 1 shot per 2 seconds
        if (Time.time >= lastAttackTime + attackDelay)
        {
            lastAttackTime = Time.time;
            SpawnProjectile(enemy);
        }
    }

    private void SpawnProjectile(Transform target)
    {
        Vector3 spawnPosition = towerTransform.position + spawnOffset;

        GameObject projectile = ObjectPool.Instance.GetObject(config.projectilePrefab.name, spawnPosition, Quaternion.identity);
        if (projectile != null)
        {
            ProjectileBase projectileScript = projectile.GetComponent<ProjectileBase>();
            projectileScript.Initialize(target, config.canDealCriticalDamage, config.projectilePrefab.GetComponent<ProjectileBase>().config);
        }
    }
}
