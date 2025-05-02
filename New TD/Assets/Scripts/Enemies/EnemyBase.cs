using UnityEngine;

/// <summary>
/// Base class for all enemy types.
/// Handles initialization, movement, health, and death events.
/// </summary>
public abstract class EnemyBase : MonoBehaviour
{
   // [SerializeField] private GameObject healthBarPrefab;
    // These fields store references to enemy components that handle health and movement.
    // Using interfaces allows flexibility, making it easy to swap different implementations
    // without modifying this class.
    public IEnemyHealth EnemyHealth;
    public IEnemyMovement EnemyMovement;
    public IEnemyAttack EnemyAttack;

    public EnemyConfig Config; // Enemy configuration settings
    private bool isInitialized = false;
    private EnemyStatusBar statusBarInstance;

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

        EnemyAttack = GetComponent<EnemyAttack>();
        EnemyAttack?.Initialize(config, transform);

        statusBarInstance = GetComponentInChildren<EnemyStatusBar>();
        if (statusBarInstance != null)
        {
            statusBarInstance.Initialize(transform, statusBarInstance.Offset);
            statusBarInstance.UpdateHealth(EnemyHealth.Current, EnemyHealth.Max);
            statusBarInstance.UpdateArmor(EnemyHealth.TotalArmor, EnemyHealth.MaxArmor);
        }

        // Subscribe to the death event of the health component
        EnemyHealth.OnDeathEvent += OnDeath;
        EnemyMovement.OnReachDestination += OnReachedEnd;
    }

    // Move the enemy if movement component exists
    private void Update()
    {
        EnemyMovement?.MoveTowards();
        EnemyAttack?.Update();
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
        UpdateStatusBars();
    }

    private void UpdateStatusBars()
    {

        if (statusBarInstance != null)
        {
            statusBarInstance.UpdateHealth(EnemyHealth.Current, EnemyHealth.Max);
            statusBarInstance.UpdateArmor(EnemyHealth.TotalArmor, EnemyHealth.MaxArmor);
        }
        else
        {
            Debug.LogWarning("HealthBarInstance is NULL");
        }
    }

    private void OnReachedEnd()
    {
        // Enemy reached the castle — now stands and attacks, no pooling
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
        CurrencyManager.Instance?.EarnMoney(Config.goldReward);
        ObjectPool.Instance.ReturnObject(gameObject, Config.enemyName);
    }

    public void ResetEnemy()
    {
        EnemyHealth?.Reset();
        UpdateStatusBars();
    }
}