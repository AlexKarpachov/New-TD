using System;

public interface IEnemyMovement
{
    void MoveTowards();
    event Action OnReachDestination;
}
