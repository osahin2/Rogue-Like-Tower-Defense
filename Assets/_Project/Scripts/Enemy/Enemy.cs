using UnityEngine;
using Rogue_Enemy.Factory;

namespace Rogue_Enemy
{
    public abstract class Enemy : MonoBehaviour, IAttack, IHit
    {
        [SerializeField] protected EnemyType _enemyType;
        public virtual int Damage { get; }

        public EnemyType EnemyType => _enemyType;

        protected IGenericEnemyFactory _factory;

        public void InitFactory(IGenericEnemyFactory enemyFactory)
        {
            _factory = enemyFactory;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        public virtual void Move(Transform target)
        {

        }
        public virtual void PoolOnFree()
        {
            gameObject.SetActive(false);
        }

        public virtual void PoolOnDestroyed()
        {
            Destroy(gameObject);
        }

        public virtual void PoolOnGet()
        {
            gameObject.SetActive(true);
        }

        public void PoolInitialSpawned()
        {
            gameObject.SetActive(false);
        }
        public abstract void Attack();

        public void Hit(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
}
