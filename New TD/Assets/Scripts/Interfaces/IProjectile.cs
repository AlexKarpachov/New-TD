public interface IProjectile
{
    void MoveTowards(IEnemyHealth target); 
    void ApplyDamage(IEnemyHealth target); 
}
