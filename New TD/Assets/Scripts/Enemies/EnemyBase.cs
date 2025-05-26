using UnityEngine;

/// <summary>
/// Base class for all enemy types.
/// Handles initialization, movement, health, and death events.
/// </summary>
public abstract class EnemyBase : MonoBehaviour
{
    public IEnemyHealth EnemyHealth;
    public IEnemyMovement EnemyMovement;
    public IEnemyAttack EnemyAttack;

    public EnemyConfig Config; 
    private bool isInitialized = false;
    private bool isDead = false;
    private EnemyStatusBar statusBarInstance;

    public void Initialize(EnemyConfig config, Transform transform, Transform[] waypoints)
    {
        if (isInitialized) return;

        isInitialized = true;
        isDead = false;

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

        EnemyHealth.OnDeathEvent += OnDeath;
        EnemyMovement.OnReachDestination += OnReachedEnd;
    }

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
        // Enemy reached the castle — now stands and attacks
    }

    private void OnDestroy()
    {
        if (EnemyHealth != null)
        {
            EnemyHealth.OnDeathEvent -= OnDeath;
        }

        if (EnemyMovement != null)
        {
            EnemyMovement.OnReachDestination -= OnReachedEnd;
        }
    }

    public virtual void OnDeath()
    {
        if (isDead) return; 
        isDead = true;

        CurrencyManager.Instance?.EarnMoney(Config.goldReward);
        ObjectPool.Instance.ReturnObject(gameObject, Config.enemyName);
    }

    public void ResetEnemy()
    {
        EnemyHealth?.Reset();
        UpdateStatusBars();
        isDead = false; 
    }
}
