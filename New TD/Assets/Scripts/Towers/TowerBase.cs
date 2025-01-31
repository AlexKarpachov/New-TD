using UnityEngine;

public abstract class TowerBase : MonoBehaviour, ITowerAttack, ITowerUpgrade, ITowerSell
{
    public abstract void Shoot(IEnemyHealth target);
    public abstract void Upgrade();
    public abstract void Sell();
}
