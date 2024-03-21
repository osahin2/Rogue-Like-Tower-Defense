using Pool;
using UnityEngine;

namespace Rogue_Enemy.Factory
{

    [CreateAssetMenu(fileName = "EnemyFactory", menuName = "Enemy/Generic Enemy Factory")]
    public class GenericEnemyFactory : ScriptableObject, IGenericEnemyFactory
    {
        [SerializeField] private PoolItem<Enemy> _enemy;

        public Enemy Enemy => _enemy.poolObject;
        public EnemyType EnemyType => Enemy.EnemyType;

        private IPooler<Enemy> _pooler;
        private Transform _poolSpawnParent;

        public void Construct(Transform poolSpawnParent)
        {
            _poolSpawnParent = poolSpawnParent;
            _pooler = new Pooler<Enemy>(_enemy, CreateEnemy, OnGet, OnFree, OnDestroyed, OnInitialSpawned);
        }
        public IEnemy GetEnemy()
        {
            return _pooler.GetPooled();
        }
        private Enemy CreateEnemy()
        {
            var enemy = Instantiate(Enemy, _poolSpawnParent);
            enemy.Construct(SetFree);
            return enemy;
        }

        private void SetFree(Enemy enemy)
        {
            _pooler.Free(enemy);
        }

        private void OnFree(Enemy enemy)
        {
            enemy.PoolOnFree();
        }
        private void OnDestroyed(Enemy enemy)
        {
            enemy.PoolOnDestroyed();
        }
        private void OnGet(Enemy enemy)
        {
            enemy.PoolOnGet();
        }

        private void OnInitialSpawned(Enemy enemy)
        {
            enemy.PoolInitialSpawned();
        }

    }
}
