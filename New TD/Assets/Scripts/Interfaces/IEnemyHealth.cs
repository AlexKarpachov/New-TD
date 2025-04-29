using System;

/// <summary>
/// Interface for enemy health system.
/// Provides access to health values, armor values and damage handling.
/// </summary>
public interface IEnemyHealth
{
    int Current { get; }
    int Max { get; }

    int TotalArmor { get; }
    int MaxArmor { get; }

    event Action OnDeathEvent;

    void TakeDamage(int damage, DamageType damageType);
}
