using System;
using UnityEngine;

/// <summary>
/// Manages enemy health, armor, and death logic.
/// </summary>
public class EnemyHealth : IEnemyHealth
{
    private int _health;
    private int _mechanicalResistance;
    private int _magicalResistance;

    public event Action OnDeathEvent; // Event triggered when enemy dies

    public EnemyHealth(int health, int mechanicalResistance, int magicalResistance)
    {
        _health = health;
        _mechanicalResistance = mechanicalResistance;
        _magicalResistance = magicalResistance;
    }

    // Applies damage to the enemy, considering armor reduction.
    public void TakeDamage(int damage, DamageType damageType)
    {
        int remainingDamage = damage;

        if (damageType == DamageType.Mechanical)
        {
            if (_mechanicalResistance > 0)
            {
                int reduction = Mathf.Min(_mechanicalResistance, damage);
                _mechanicalResistance -= reduction;
                remainingDamage -= reduction;
            }
        }
        else if (damageType == DamageType.Magical)
        {
            if (_magicalResistance > 0)
            {
                int reduction = Mathf.Min(_magicalResistance, damage);
                _magicalResistance -= reduction;
                remainingDamage -= reduction;
            }
        }

        if (remainingDamage > 0)
        {
            _health -= remainingDamage;
        }

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
        OnDeathEvent?.Invoke(); // Notify subscribers (e.g., EnemyBase)
    }
}
