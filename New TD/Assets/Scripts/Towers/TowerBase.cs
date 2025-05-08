using UnityEngine;

/// <summary>
/// Base class for all towers. Manages attack, upgrade, and sell.
/// </summary>
public class TowerBase : MonoBehaviour
{
    [SerializeField] private TowerConfig config;
    private ITowerAttack attackSystem;
    private TowerUpgradeSell upgradeSellSystem;
    private TowerTargeting targetingSystem;

    private float checkInterval = 0.5f;
    private float lastCheckTime = 0f;

    private void Start()
    {
        targetingSystem = new TowerTargeting(config, transform);
        attackSystem = new TowerAttack(config, transform);
        upgradeSellSystem = new TowerUpgradeSell(config);
    }

    private void Update()
    {
        if (Time.time >= lastCheckTime + checkInterval)
        {
            lastCheckTime = Time.time;
            Transform nearestEnemy = targetingSystem.FindNearestEnemy();
            if (nearestEnemy != null)
            {
                attackSystem.Attack(nearestEnemy);
            }
        }
    }

    /// <summary>
    /// Virtual method to allow child classes to override attack behavior.
    /// By default, it calls the attack system of the tower.
    /// This method is marked as virtual because different tower types 
    /// (e.g., SniperTower, FlameTower) may have unique attack mechanics.
    /// </summary>
    public virtual void Attack(Transform enemy)
    {
        attackSystem.Attack(enemy);
    }

    /// <summary>
    /// Upgrades the tower. 
    /// This method is NOT virtual because the upgrade logic 
    /// is the same for all towers and is handled by TowerUpgradeSell.
    /// If a unique upgrade system is needed, modifying TowerUpgradeSell 
    /// is preferable over overriding this method.
    /// </summary>
    public void Upgrade()
    {
        upgradeSellSystem.Upgrade(gameObject);
    }

    /// <summary>
    /// Sells the tower, returning its sell value to the player.
    /// This method is NOT virtual because all towers follow the same selling logic.
    /// The logic is managed by TowerUpgradeSell, ensuring consistency.
    /// </summary>
    public void Sell()
    {
        upgradeSellSystem.Sell(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (config == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, config.range);
    }
}
