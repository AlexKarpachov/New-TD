using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerConfig", menuName = "Configs/TowerConfig")]
public class TowerConfig : ScriptableObject
{
    public string towerName;
    public int purchaseCost;
    public int sellValue; 
    public int upgradeCost; 
    public float range; // Shoot radius
    public float fireRate; // Shoot frequency
    public GameObject projectilePrefab; // Projectile type
    public bool canDealCriticalDamage; // does the tower can make crit damage or not
    public GameObject prefab; // tower prefab
    public TowerConfig nextUpgrade; // updated tower config. If null - the tower has its max upgrade (or doesn't have at all)
}
