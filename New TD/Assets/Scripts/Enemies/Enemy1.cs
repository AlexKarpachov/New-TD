using UnityEngine;

public class Enemy1 : EnemyBase
{
    void Start()
    {
        Waypoints waypoints = GameObject.FindGameObjectWithTag("Route1").GetComponent<Waypoints>();
        Initialize(Config, transform, waypoints.points);
    }
}
