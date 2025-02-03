using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected IEnemyHealth EnemyHealth;
    protected IEnemyMovement EnemyMovement;
    public EnemyConfig Config;

    public void Initialize(EnemyConfig config, Transform transform, Transform[] waypoints)
    {
        Config = config;
        EnemyHealth = new EnemyHealth(config.health, config.armor);
        EnemyMovement = new EnemyMovement(transform, config.speed, waypoints);
        EnemyHealth.OnDeathEvent += OnDeath;
    }

    public virtual void TakeDamage(int damage, DamageType damageType)
    {
        EnemyHealth.TakeDamage(damage, damageType);
    }

    public virtual void MoveTowards(Vector3 destination)
    {
        EnemyMovement.MoveTowards(destination);
    }

    private void OnDestroy()
    {
        EnemyHealth.OnDeathEvent -= OnDeath;
    }

    public virtual void OnDeath()
    {
        EnemyHealth.OnDeath();
        Destroy(gameObject);
    }
}