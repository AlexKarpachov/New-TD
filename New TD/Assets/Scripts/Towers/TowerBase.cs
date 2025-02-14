using UnityEngine;

public abstract class TowerBase : MonoBehaviour, ITowerAttack, ITowerUpgrade, ITowerSell
{
    [SerializeField] protected TowerConfig config;
    protected Transform target;
    protected float attackCooldown;

    private void Update()
    {
        if (target != null)
        {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0f)
            {
                Attack();
                attackCooldown = config.fireRate;
            }
        }
    }

    public virtual void Attack()
    {
        Debug.Log($"{gameObject.name} is attacking {target.name}!");
    }

    public virtual void Upgrade()
    {
        if (config.nextUpgrade == null)
        {
            Debug.Log("No further upgrades available!");
            return;
        }

        // Отримуємо позицію та кут старої вежі
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        // Видаляємо поточну вежу
        Destroy(gameObject);

        // Створюємо нову вежу на тому ж місці
        GameObject newTower = Instantiate(config.nextUpgrade.prefab, position, rotation);
        Debug.Log($"{gameObject.name} upgraded to {config.nextUpgrade.towerName}!");
    }

    public virtual void Sell()
    {
        Debug.Log($"{gameObject.name} sold for {config.sellValue} coins!");
        Destroy(gameObject);
    }
}
