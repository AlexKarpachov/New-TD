using System;
using UnityEngine;

/// <summary>
/// Manages enemy health, armor, and death logic.
/// </summary>
public class EnemyHealth : IEnemyHealth
{
    private int _health;
    private int _armor;

    public event Action OnDeathEvent; // Event triggered when enemy dies

    public EnemyHealth(int health, int armor)
    {
        _health = health;
        _armor = armor;
    }

    // Applies damage to the enemy, considering armor reduction.
    public void TakeDamage(int damage, DamageType damageType)
    {
        // Reduce damage based on armor, ensuring it doesn't go below zero
        int adjustedDamage = Mathf.Max(0, damage - _armor);
        _health -= adjustedDamage;
        Debug.Log($"Enemy took {adjustedDamage} damage. Remaining health: {_health}");

        if (_health <= 0)
        {
            OnDeath();
        }
    }

    // Returns the enemy's current health
    public int GetHealth()
    {
        return _health;
    }

    public void OnDeath()
    {
        Debug.Log("Enemy has died!");
        OnDeathEvent?.Invoke(); // Notify subscribers (e.g., EnemyBase)
    }
}
