using UnityEngine;

public class EnemyMovement : IEnemyMovement
{
    private Transform _transform;
    private float _speed;
    private Transform[] _waypoints;
    private int _currentWaypointIndex = 0;

    public EnemyMovement(Transform transform, float speed, Transform[] waypoints)
    {
        _transform = transform;
        _speed = speed;
        _waypoints = waypoints;
    }
    public void MoveTowards(Vector3 destination)
    {
        if (_currentWaypointIndex >= _waypoints.Length) return;

        Transform targetWaypoint = _waypoints[_currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - _transform.position).normalized;
        _transform.position += direction * _speed * Time.deltaTime;

        if (Vector3.Distance(_transform.position, targetWaypoint.position) < 0.1f)
        {
            _currentWaypointIndex++;
        }
    }
}
