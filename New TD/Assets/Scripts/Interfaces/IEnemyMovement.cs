using System;

public interface IEnemyMovement
{
    void MoveTowards();
    void Stop();
    event Action OnReachDestination;
}
