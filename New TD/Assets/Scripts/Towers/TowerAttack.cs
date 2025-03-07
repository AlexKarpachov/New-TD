using UnityEngine;

/// <summary>
/// Handles tower attack logic.
/// </summary>
public class TowerAttack : ITowerAttack
{
    private TowerConfig config;
    private float lastAttackTime = 0f;
    private Transform towerTransform;

    public TowerAttack(TowerConfig config, Transform towerTransform)
    {
        this.config = config;
        this.towerTransform = towerTransform;
    }

    public void Attack(Transform enemy)
    {
        if (enemy == null) return;

        if (Time.time >= lastAttackTime + 1f / config.fireRate)
        {
            lastAttackTime = Time.time;
            SpawnProjectile(enemy);
        }
    }

    private void SpawnProjectile(Transform target)
    {
        GameObject projectile = ObjectPool.Instance.GetObject(config.projectilePrefab.name, towerTransform.position, Quaternion.identity);
        if (projectile != null)
        {
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.Initialize(target, config.canDealCriticalDamage, config.projectilePrefab.GetComponent<Projectile>().config);
        }
    }
}
