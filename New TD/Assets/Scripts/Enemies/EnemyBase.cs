using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected abstract void TakeDamage(int damage, DamageType damageType); //— делегує роботу HealthComponent.
    protected abstract void MoveTowards(Vector3 destination); //— делегує роботу MovementComponent.
    protected abstract void OnDeath(); // — делегує роботу HealthComponent.
}
