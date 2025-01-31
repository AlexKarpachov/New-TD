using UnityEngine;

public class EnemyMovement : IEnemyMovement
{
    private Transform _transform;
    private float _speed;

    public EnemyMovement(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }
    public void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - _transform.position).normalized;
        _transform.position += direction * _speed * Time.deltaTime;
    }
}
