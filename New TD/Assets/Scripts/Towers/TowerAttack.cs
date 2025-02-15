using UnityEngine;

/// <summary>
/// Handles tower attack logic.
/// </summary>
public class TowerAttack : ITowerAttack
{
    private TowerConfig config;
    private float lastAttackTime = 0f;

    public TowerAttack(TowerConfig config)
    {
        this.config = config;
    }

    public void Attack(Transform enemy)
    {
        if (Time.time >= lastAttackTime + 1f / config.fireRate)
        {
            lastAttackTime = Time.time;

            // Instantiate projectile and direct it at enemy
            GameObject projectile = Object.Instantiate(config.projectilePrefab, enemy.position, Quaternion.identity);
            Debug.Log($"Tower attacked {enemy.name}!");
        }
    }
}
