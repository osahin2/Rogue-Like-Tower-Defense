namespace Rogue_Enemy.Factory
{
    public interface IEnemyFactory
    {
        void Init();
        IEnemy Get(EnemyType enemy);

    }
}
