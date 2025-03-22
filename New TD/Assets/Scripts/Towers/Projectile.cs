using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileConfig config;
    private Transform target;
    private bool isCriticalHit;

    public void Initialize(Transform target, bool canDealCritical, ProjectileConfig projectileConfig)
    {
        this.target = target;
        this.isCriticalHit = canDealCritical && Random.value > 0.5f; 
    }

    private void Update()
    {
        if (target == null)
        {
            ObjectPool.Instance.ReturnObject(gameObject, config.projectileName);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, config.speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            ApplyDamage(target);
            ObjectPool.Instance.ReturnObject(gameObject, config.projectileName);
        }
    }

    private void ApplyDamage(Transform enemy)
    {
        EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
        if (enemyBase != null)
        {
            IEnemyHealth enemyHealth = enemyBase.EnemyHealth;
            if (enemyHealth != null)
            {
                int damage = isCriticalHit ? Mathf.RoundToInt(config.damage * 1.5f) : config.damage;
                enemyHealth.TakeDamage(damage, config.damageType);
            }
        }
    }
}
