public interface IEnemyHealth
{
    void TakeDamage(int damage, DamageType damageType);

    int GetHealth();

    void OnDeath();
}
