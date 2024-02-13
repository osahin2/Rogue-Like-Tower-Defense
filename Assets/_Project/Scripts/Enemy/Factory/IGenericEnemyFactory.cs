using UnityEngine;

namespace Rogue_Enemy.Factory
{
    public interface IGenericEnemyFactory
    {
        public EnemyType EnemyType { get;}
        void Construct(Transform poolSpawnParent);
        Enemy GetEnemy();
        void Free(Enemy enemy);
    }
}
