// ProjectileBase.cs
using UnityEngine;

/// <summary>
/// Abstract base class for all projectiles. Handles movement, rotation, and lifecycle.
/// </summary>
public abstract class ProjectileBase : MonoBehaviour
{
    public ProjectileConfig config;
    protected Transform target;
    protected bool isCriticalHit;

    Vector3 hitTargetOffset = new Vector3(0, 1, 0);

    public virtual void Initialize(Transform target, bool canDealCritical, ProjectileConfig projectileConfig)
    {
        this.target = target;
        config = projectileConfig;
        isCriticalHit = canDealCritical && Random.value > 0.5f;
    }

    protected virtual void Update()
    {
        if (target == null)
        {
            ObjectPool.Instance.ReturnObject(gameObject, config.projectileName);
            return;
        }

        RotateTowardsTarget();
        MoveTowardsTarget();

        if (Vector3.Distance(transform.position, target.position + hitTargetOffset) < 0.1f)
        {
            OnHit(target);
            ObjectPool.Instance.ReturnObject(gameObject, config.projectileName);
        }
    }

    protected virtual void RotateTowardsTarget()
    {
        Vector3 direction = (target.position + hitTargetOffset - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
        }
    }

    protected virtual void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + hitTargetOffset, config.speed * Time.deltaTime);
    }

    protected abstract void OnHit(Transform enemy);
}
