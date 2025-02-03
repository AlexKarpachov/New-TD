using System;
public interface IEnemyHealth
{
    event Action OnDeathEvent;  
    void TakeDamage(int damage, DamageType damageType);

    int GetHealth();

    void OnDeath();
}
