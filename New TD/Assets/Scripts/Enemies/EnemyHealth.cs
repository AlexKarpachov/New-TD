using System;
using UnityEngine;

public class EnemyHealth : IEnemyHealth
{
    private int _health;
    private int _armor;

    public event Action OnDeathEvent;

    public EnemyHealth(int health, int armor)
    {
        _health = health;
        _armor = armor;
    }

    public void TakeDamage(int damage, DamageType damageType)
    {
        int adjustedDamage = Mathf.Max(0, damage - _armor);
        _health -= adjustedDamage;
        Debug.Log($"Enemy took {adjustedDamage} damage. Remaining health: {_health}");

        if (_health <= 0)
        {
            OnDeath();
        }
    }

    public int GetHealth()
    {
        return _health;
    }

    public void OnDeath()
    {
        Debug.Log("Enemy has died!");
        OnDeathEvent?.Invoke();
        // Додаткові дії при смерті ворога (наприклад, видалення об'єкта)
    }
}
