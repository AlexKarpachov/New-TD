using System;
using UnityEngine;

/// <summary>
/// Manages enemy health, armor, and death logic.
/// </summary>
public class EnemyHealth : IEnemyHealth
{
    public event Action OnDeathEvent;

    public int Current { get; private set; }
    public int Max { get; private set; }

    private int _mechanicalResistance;
    private int _magicalResistance;

    public EnemyHealth(int health, int mechanicalResistance, int magicalResistance)
    {
        Max = health;
        Current = health;
        _mechanicalResistance = mechanicalResistance;
        _magicalResistance = magicalResistance;
    }

    public void TakeDamage(int damage, DamageType damageType)
    {
        int remainingDamage = damage;

        if (damageType == DamageType.Mechanical && _mechanicalResistance > 0)
        {
            int reduction = Mathf.Min(_mechanicalResistance, damage);
            _mechanicalResistance -= reduction;
            remainingDamage -= reduction;
        }
        else if (damageType == DamageType.Magical && _magicalResistance > 0)
        {
            int reduction = Mathf.Min(_magicalResistance, damage);
            _magicalResistance -= reduction;
            remainingDamage -= reduction;
        }

        if (remainingDamage > 0)
        {
            Current -= remainingDamage;
        }

        if (Current <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        OnDeathEvent?.Invoke();
    }
}
