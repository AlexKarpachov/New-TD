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

    private void Start()
    {
        attackSystem = new TowerAttack(config);
        upgradeSystem = new TowerUpgradeSell(config);
        sellSystem = new TowerUpgradeSell(config);
    }

    public void Attack(Transform enemy)
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
