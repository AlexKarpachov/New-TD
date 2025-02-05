using UnityEngine;

// Handles enemy movement along a predefined path.
public class EnemyMovement : IEnemyMovement
{
    private Transform _transform; // Enemy's transform reference
    private float _speed;
    private Transform[] _waypoints; // List of waypoints for the enemy to follow
    private int _currentWaypointIndex = 0; // Tracks the current waypoint the enemy is moving toward

    public EnemyMovement(Transform transform, float speed, Transform[] waypoints)
    {
        _transform = transform;
        _speed = speed;
        _waypoints = waypoints;
    }

    // Moves the enemy towards the next waypoint in the path.
    public void MoveTowards()
    {
        if (_waypoints.Length == 0 || _currentWaypointIndex >= _waypoints.Length) return;

        // Get the target waypoint
        Transform targetWaypoint = _waypoints[_currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - _transform.position).normalized;

        // Move enemy in the direction of the waypoint
        _transform.position += direction * _speed * Time.deltaTime;

        // If close enough to the waypoint, switch to the next waypoint
        if (Vector3.Distance(_transform.position, targetWaypoint.position) < 0.1f)
        {
            _currentWaypointIndex++;
        }
    }
}
