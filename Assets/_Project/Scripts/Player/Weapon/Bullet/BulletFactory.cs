using Pool;

namespace Player.Weapons.Bullets
{
    public class BulletFactory : IBulletFactory
    {
        private BulletConfig _bulletConfig;
        private IPooler<Bullet> _bulletPooler;

        public BulletFactory(BulletConfig bulletConfig)
        {
            _bulletConfig = bulletConfig;

            _bulletPooler = new Pooler<Bullet>(
                _bulletConfig.BulletPoolItem,
                OnBulletCreate,
                OnBulletGet,
                OnBulletFree,
                OnBulletDestroyed,
                OnInitiallySpawned);
        }
        private Bullet OnBulletCreate()
        {
            var bullet = _bulletConfig.CreateBullet();
            bullet.Construct(_bulletConfig, FreeBullet);
            return bullet;
        }
        private void OnBulletGet(Bullet bullet)
        {
            bullet.OnGet();
        }
        private void OnBulletFree(Bullet bullet)
        {
            bullet.OnFree();
        }
        private void OnBulletDestroyed(Bullet bullet)
        {
            bullet.OnDestroyed();
        }
        private void OnInitiallySpawned(Bullet bullet)
        {
            bullet.InitiallySpawnedFromPooler();
        }
        public IBullet CreateBullet()
        {
            return _bulletPooler.GetPooled();
        }

        private void FreeBullet(Bullet bullet)
        {
            _bulletPooler.Free(bullet);
        }
    }
}