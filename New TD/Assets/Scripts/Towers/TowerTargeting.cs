using System.Collections.Generic;
using UnityEngine;

public class TowerTargeting
{
    private TowerConfig config;
    private Transform towerTransform;

    public TowerTargeting(TowerConfig config, Transform towerTransform)
    {
        this.config = config;
        this.towerTransform = towerTransform;
    }

    public Transform FindNearestEnemy()
    {
        if (EnemyManager.Instance == null) 
        {
            return null;
        }

        List<Transform> enemies = EnemyManager.Instance.GetActiveEnemies();

        if (enemies == null || enemies.Count == 0) 
        {
            return null;
        }

        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform enemy in enemies)
        {
            float distance = Vector3.Distance(towerTransform.position, enemy.position);
            if (distance < shortestDistance && distance <= config.range)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
