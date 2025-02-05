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

    private void Update()
    {
        if (EnemyMovement != null)
        {
            EnemyMovement.MoveTowards();
        }
    }

    public virtual void TakeDamage(int damage, DamageType damageType)
    {
        EnemyHealth.TakeDamage(damage, damageType);
    }

    public virtual void MoveTowards(Vector3 destination)
    {
        EnemyMovement.MoveTowards();
    }

    private void OnDestroy()
    {
        EnemyHealth.OnDeathEvent -= OnDeath;
    }

    public virtual void OnDeath()
    {
        EnemyHealth.OnDeath();
        ObjectPoolManager.Instance.ReturnObject(gameObject, "enemy1"); 
    }
}