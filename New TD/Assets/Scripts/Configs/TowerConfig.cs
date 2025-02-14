using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerConfig", menuName = "Configs/TowerConfig")]
public class TowerConfig : ScriptableObject
{
    public string towerName;
    public int purchaseCost; // Ціна покупки
    public int sellValue; // Вартість продажу
    public int upgradeCost; // Вартість покращення
    public float range; // Радіус дії
    public float fireRate; // Частота стрільби
    public GameObject projectilePrefab; // Тип снаряда
    public bool canDealCriticalDamage; // Критичний урон
    public GameObject prefab; // Префаб вежі
    public TowerConfig nextUpgrade; // Покращений варіант цієї вежі
}
