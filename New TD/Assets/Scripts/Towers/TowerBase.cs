using UnityEngine;

/// <summary>
/// Base class for all towers. Manages attack, upgrade, and sell.
/// </summary>
public class TowerBase : MonoBehaviour
{
    [SerializeField] private TowerConfig config;
    private ITowerAttack attackSystem;
    private ITowerUpgrade upgradeSystem;
    private ITowerSell sellSystem;
    private TowerTargeting targetingSystem;

    private float checkInterval = 0.5f;
    private float lastCheckTime = 0f;

    private void Start()
    {
        targetingSystem = new TowerTargeting(config, transform);
        attackSystem = new TowerAttack(config, transform);
        upgradeSystem = new TowerUpgradeSell(config);
        sellSystem = new TowerUpgradeSell(config);
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

    public virtual void Attack(Transform enemy)
    {
        attackSystem.Attack(enemy);
    }

    public void Upgrade()
    {
        upgradeSystem.Upgrade(gameObject);
    }

    public void Sell()
    {
        sellSystem.Sell(gameObject);
    }
}
