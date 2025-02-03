public interface IProjectile
{
    void MoveTowards(IEnemyHealth target); // — рух до ворога.
    void ApplyDamage(IEnemyHealth target); // — наносить урон ворогу
}
