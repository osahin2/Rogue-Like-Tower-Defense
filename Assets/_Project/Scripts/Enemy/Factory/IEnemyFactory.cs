namespace Rogue_Enemy.Factory
{
    public interface IEnemyFactory
    {
        void Init();
        Enemy Get(EnemyType enemy);
        void Release(Enemy enemy);

    }
}
