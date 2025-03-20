using UnityEngine;

/// <summary>
/// Handles tower upgrade and sell logic.
/// </summary>
public class TowerUpgradeSell : ITowerUpgrade, ITowerSell
{
    private TowerConfig config;

    public TowerUpgradeSell(TowerConfig config)
    {
        this.config = config;
    }

    public void Upgrade(GameObject currentTower)
    {
        if (config.nextUpgrade == null)
        {
            return;
        }

        if (!CurrencyManager.Instance.CanAfford(config.upgradeCost))
        {
            Debug.Log("Not enough money to upgrade!");
            return;
        }

        CurrencyManager.Instance.SpendMoney(config.upgradeCost);

        Vector3 position = currentTower.transform.position;
        Quaternion rotation = currentTower.transform.rotation;

        Object.Destroy(currentTower);
        Object.Instantiate(config.nextUpgrade.prefab, position, rotation);
    }

    public void Sell(GameObject currentTower)
    {
        CurrencyManager.Instance.EarnMoney(config.sellValue);
        Object.Destroy(currentTower);
    }
}
