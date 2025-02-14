using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private int money = 100; // Початковий баланс

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool CanAfford(int cost)
    {
        return money >= cost;
    }

    public void SpendMoney(int amount)
    {
        if (CanAfford(amount))
        {
            money -= amount;
            Debug.Log($"Spent {amount} coins. Remaining: {money}");
        }
    }

    public void EarnMoney(int amount)
    {
        money += amount;
        Debug.Log($"Earned {amount} coins. Total: {money}");
    }
}
