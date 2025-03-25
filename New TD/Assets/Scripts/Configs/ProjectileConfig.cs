using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileConfig", menuName = "Configs/ProjectileConfig")]
public class ProjectileConfig : ScriptableObject
{
    public string projectileName;
    public float speed;
    public int damage;
    public bool isAreaDamage;          // Whether or not makes damage to an area
    public float explosionRadius;      // AOE radius
    public DamageType damageType; // Mechanical or Magical.
    public GameObject visualEffectPrefab;
}