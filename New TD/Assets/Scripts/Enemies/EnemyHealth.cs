using System;
using UnityEngine;

/// <summary>
/// Manages enemy health, armor, and death logic.
/// Tracks mechanical and magical resistances separately,
/// and allows UI to query armor state for display.
/// </summary>
public class EnemyHealth : IEnemyHealth
{
    public event Action OnDeathEvent;

    public int Current { get; private set; }
    public int Max { get; private set; }

    private int _mechanicalResistance;
    private int _magicalResistance;

    private readonly int _initialMechanical;
    private readonly int _initialMagical;

    public int TotalArmor => _mechanicalResistance + _magicalResistance;
    public int MaxArmor => _initialMechanical + _initialMagical;

    public EnemyHealth(int health, int mechanicalResistance, int magicalResistance)
    {
        Max = health;
        Current = health;

        _mechanicalResistance = mechanicalResistance;
        _magicalResistance = magicalResistance;

        _initialMechanical = mechanicalResistance;
        _initialMagical = magicalResistance;
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
            int oldHealth = Current;
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

    public void Reset()
    {
        Current = Max;
        _mechanicalResistance = _initialMechanical;
        _magicalResistance = _initialMagical;
    }
}
