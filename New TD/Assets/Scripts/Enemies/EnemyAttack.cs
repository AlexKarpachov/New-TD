using UnityEngine;

public class EnemyAttack : MonoBehaviour, IEnemyAttack
{
    private EnemyConfig config;
    private CastleHealth targetCastle;
    // private Transform enemyTransform;
    private float cooldown = 1f;
    private float lastAttackTime = 0f;

    public void Initialize(EnemyConfig config, Transform enemyTransform)
    {
        this.config = config;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Castle"))
        {
            if (targetCastle == null)
            {
                targetCastle = other.GetComponent<CastleHealth>();
            }

            GetComponent<EnemyBase>()?.EnemyMovement?.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!enabled || targetCastle == null) return;

        if (other.CompareTag("Castle") && Time.time >= lastAttackTime + cooldown)
        {
            lastAttackTime = Time.time;
            targetCastle.TakeDamage(config.livesDamage);
        }
    }

    public void Update()
    {
        /*if (targetCastle == null) return;

        float distance = Vector3.Distance(enemyTransform.position, targetCastle.transform.position);
        if (distance <= 1.5f && Time.time >= lastAttackTime + cooldown)
        {
            lastAttackTime = Time.time;
            targetCastle.TakeDamage(config.livesDamage);
        }*/
    }
}
