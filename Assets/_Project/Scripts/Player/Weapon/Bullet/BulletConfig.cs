using Pool;
using UnityEngine;

namespace Player.Weapons.Bullets
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "Weapon/Bullet Config")]
    public class BulletConfig : ScriptableObject
    {
        [SerializeField] private PoolItem<Bullet> _bulletPoolItem;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletDampen;

        public float BulletSpeed => _bulletSpeed;
        public float BulletDampen => _bulletDampen;
        public PoolItem<Bullet> BulletPoolItem => _bulletPoolItem;
        public Bullet CreateBullet()
        {
            return Instantiate(_bulletPoolItem.poolObject);
        }
    }
}