using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected IEnemyHealth EnemyHealth;
    protected IEnemyMovement EnemyMovement;
    public EnemyConfig Config;

    public void Initialize(EnemyConfig config, Transform transform)
    {
        Config = config;
        EnemyHealth = new EnemyHealth(config.health, config.armor);
        EnemyMovement = new EnemyMovement(transform, config.speed);
    }

    public virtual void TakeDamage(int damage, DamageType damageType)
    {
        EnemyHealth.TakeDamage(damage, damageType);

        if (EnemyHealth.GetHealth() <= 0)
        {
            OnDeath();
        }
    }

    public virtual void MoveTowards(Vector3 destination)
    {
        EnemyMovement.MoveTowards(destination);
    }

    public virtual void OnDeath()
    {
        EnemyHealth.OnDeath();
        Destroy(gameObject);
    }
}

//// подивитися останню відповід в чаті та переробити здоров'я та базовий класи. було питання щодо івентів смерті