using System;
using UnityEngine;

// Handles enemy movement along a predefined path.
public class EnemyMovement : IEnemyMovement
{
    Transform _transform; // Enemy's transform reference
    Transform[] _waypoints; // List of waypoints for the enemy to follow
    float _speed;
    private bool isStopped = false;
    float rotationSpeed = 3f;
    int _currentWaypointIndex = 0; // Tracks the current waypoint the enemy is moving toward

    public event Action OnReachDestination;

    public EnemyMovement(Transform transform, float speed, Transform[] waypoints)
    {
        _transform = transform;
        _speed = speed;
        _waypoints = waypoints;
    }

    public void Stop()
    {
        isStopped = true;
    }

    // Moves the enemy towards the next waypoint in the path.
    public void MoveTowards()
    {
        if (isStopped) return;

        if (_waypoints.Length == 0 || _currentWaypointIndex >= _waypoints.Length)
        {
            {
                OnReachDestination?.Invoke();
                return;
            }
        }

        // Get the target waypoint
        Transform targetWaypoint = _waypoints[_currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - _transform.position).normalized;

        // Rotate enemy towards the waypoint smoothly
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        // Move enemy in the direction of the waypoint
        _transform.position += direction * _speed * Time.deltaTime;

        // If close enough to the waypoint, switch to the next waypoint
        if (Vector3.Distance(_transform.position, targetWaypoint.position) < 0.1f)
        {
            _currentWaypointIndex++;

            if (_currentWaypointIndex >= _waypoints.Length)
            {
                _speed = 0f;
                OnReachDestination?.Invoke();
            }
        }
    }
}
