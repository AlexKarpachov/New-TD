using UnityEngine;

public class CastleHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Castle took {amount} damage! Current HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("Castle destroyed! Game Over.");
            // TODO: Handle game over state
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
