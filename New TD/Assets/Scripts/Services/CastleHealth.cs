using UnityEngine;

public class CastleHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private CastleHealthBar healthBarUI;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBarUI.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth); // safety clamp

        Debug.Log($"Castle took {amount} damage! Current HP: {currentHealth}");

        healthBarUI.UpdateHealth(currentHealth, maxHealth);

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
