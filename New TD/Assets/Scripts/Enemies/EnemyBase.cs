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
    public IEnemyHealth EnemyHealth;
    public IEnemyMovement EnemyMovement;

    public EnemyConfig Config; // Enemy configuration settings
    private bool isInitialized = false;

    /// <summary>
    /// Initializes the enemy with config data, movement component, and waypoints.
    /// </summary>
    public void Initialize(EnemyConfig config, Transform transform, Transform[] waypoints)
    {
        if (isInitialized)
        {
            return;
        }
        isInitialized = true;
        Config = config;
        EnemyHealth = new EnemyHealth(config.health, config.mechanicalResistance, config.magicalResistance);
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

    private void OnEnable()
    {
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RegisterEnemy(transform);
        }
    }

    private void OnDisable()
    {
        isInitialized = false;
        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.UnregisterEnemy(transform);
        }
    }

    // Applies damage to the enemy.
    public virtual void TakeDamage(int damage, DamageType damageType)
    {
        EnemyHealth.TakeDamage(damage, damageType);
    }

    private void OnReachedEnd()
    {
        ObjectPool.Instance.ReturnObject(gameObject, Config.enemyName);
    }

    private void OnDestroy()
    {
        if (EnemyHealth != null)
        {
            EnemyHealth.OnDeathEvent -= OnDeath; // Unsubscribe from the death event to prevent memory leaks
        }

        if (EnemyMovement != null) { EnemyMovement.OnReachDestination -= OnReachedEnd; }
    }

    // Called when the enemy dies. Handles object removal.
    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }
}