using UnityEngine;

/// <summary>
/// Concrete enemy class that inherits from EnemyBase.
/// Responsible for setting up the movement path for this specific enemy type.
/// </summary>
public class Enemy1 : EnemyBase
{
    void Start()
    {
        // Find the assigned route using relevant tag (Route1, Route2)
        Waypoints waypoints = GameObject.FindGameObjectWithTag("Route2").GetComponent<Waypoints>(); // в майбутньому реалізувати це через посиланя (Waypoints route1, route2) у WaveManager

        // Initialize enemy with its configuration, transform, and assigned waypoints. Unity sets transform value - an enemy's position
        Initialize(Config, transform, waypoints.points);
    }
}
