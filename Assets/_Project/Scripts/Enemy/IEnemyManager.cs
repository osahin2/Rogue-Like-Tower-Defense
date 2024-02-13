using Rogue_Enemy.Factory;

namespace Rogue_Enemy
{
    public interface IEnemyManager
    {
        public IEnemyFactory EnemyFactory { get; }
        public int SpawnedEnemyCount { get; }
    }
}