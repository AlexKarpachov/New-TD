using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileConfig", menuName = "Configs/ProjectileConfig")]
public class ProjectileConfig : ScriptableObject
{
    public string projectileName;
    public float speed;
    public int damage;
    public DamageType damageType; // Mechanical or Magical.
    public GameObject visualEffectPrefab;
}