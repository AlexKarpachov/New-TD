using UnityEngine;

/// <summary>
/// Base class for all enemy types.
/// Handles initialization, movement, health, and death events.
/// </summary>
public abstract class EnemyBase : MonoBehaviour
{
    // These fields store references to enemy components that handle health and movement.
    // Using interfaces allows flexibility, making it easy to swap different implementations
    // without modifying this class.
    protected IEnemyHealth EnemyHealth;
    protected IEnemyMovement EnemyMovement;

    public EnemyConfig Config; // Enemy configuration settings

    /// <summary>
    /// Initializes the enemy with config data, movement component, and waypoints.
    /// </summary>
    public void Initialize(EnemyConfig config, Transform transform, Transform[] waypoints)
    {
        Config = config;
        EnemyHealth = new EnemyHealth(config.health, config.armor);
        EnemyMovement = new EnemyMovement(transform, config.speed, waypoints);

        // Subscribe to the death event of the health component
        EnemyHealth.OnDeathEvent += OnDeath;
        EnemyMovement.OnReachDestination += OnReachedEnd;
    }

    // Move the enemy if movement component exists
    private void Update()
    {
        if (EnemyMovement != null)
        {
            EnemyMovement.MoveTowards();
        }
    }

    // Applies damage to the enemy.
    public virtual void TakeDamage(int damage, DamageType damageType)
    {
        EnemyHealth.TakeDamage(damage, damageType);
    }

    private void OnReachedEnd()
    {
        Debug.Log($"{gameObject.name} reached the end! Returning to pool.");
        ObjectPool.Instance.ReturnObject(gameObject, Config.enemyName); 
    }

    private void OnDestroy()
    {
        if (EnemyHealth != null)
        {
            EnemyHealth.OnDeathEvent -= OnDeath; // Unsubscribe from the death event to prevent memory leaks
        }
        EnemyMovement.OnReachDestination -= OnReachedEnd;
    }

    // Called when the enemy dies. Handles object removal.
    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }
}