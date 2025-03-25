using System;

/// <summary>
/// Interface for enemy health system.
/// Provides access to health values and damage handling.
/// </summary>
public interface IEnemyHealth
{
    int Current { get; }
    int Max { get; }

    event Action OnDeathEvent;

    void TakeDamage(int damage, DamageType damageType);
}
