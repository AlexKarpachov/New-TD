using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileConfig", menuName = "Configs/ProjectileConfig")]
public class ProjectileConfig : ScriptableObject
{
    public string projectileName;
    public float speed;
    public int damage;
    public bool isCritical;
    public DamageType damageType; // Наприклад, механічний або магічний.
    public GameObject visualEffectPrefab;
}
