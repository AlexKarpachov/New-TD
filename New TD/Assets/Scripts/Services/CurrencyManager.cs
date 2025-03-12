using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI balanceText;
    public static CurrencyManager Instance;

    private int money = 100; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        balanceText.text = "Money $ " + money.ToString();
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
            balanceText.text = ("Money $" + money);
        }
    }

    public void EarnMoney(int amount)
    {
        money += amount;
        balanceText.text = ("Money $" + money);
    }
}
