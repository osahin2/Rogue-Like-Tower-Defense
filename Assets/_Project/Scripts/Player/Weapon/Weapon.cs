using CountdownTimer;
using Player.Weapons.Bullets;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Player.Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Transform _leftHandPosition;
        [SerializeField] private Transform _rightHandPosition;
        [SerializeField] protected Transform _firePoint;
        [SerializeField] private Transform _crosshair;
        [SerializeField] private ParticleSystem _muzzleParticle;

        private IBulletFactory _bulletFactory;
        private WeaponConfig settings;
        private ICountdown _fireRateTimer;
        private NonAllocRaycaster _nonAllocRaycaster;

        public Vector3 LeftHandPosition => _leftHandPosition.position;
        public Vector3 RightHandPosition => _rightHandPosition.position;
        private bool AnyBulletsSpawned => _bullets.Count > 0;
        private Vector3 CrosshairDefaultPosition => _firePoint.right * settings.CurrentRange;

        private List<IBullet> _bullets = new();
        private readonly RaycastHit2D[] _bulletHitResults = new RaycastHit2D[1];
        private readonly RaycastHit2D[] _crosshairHitResults = new RaycastHit2D[1];
        private int _bulletHitLayer;


        public void Init(WeaponConfig settings)
        {
            this.settings = settings;
            _bulletFactory = new BulletFactory(settings.BulletConfig);
            _fireRateTimer = new Countdown();
            _nonAllocRaycaster = new NonAllocRaycaster();

            SetCrosshairPosition(CrosshairDefaultPosition);

            _bulletHitLayer = LayerMask.GetMask("Enemy");
        }

        public void UpdateWeapon()
        {
            Fire();
            UpdateBullets();
            UpdateCrosshair();
        }
        private void Fire()
        {
            if (_fireRateTimer.Check(Time.deltaTime, settings.CurrentFireRate))
            {
                OnFire();
            }
        }
        protected virtual void OnFire()
        {
            _muzzleParticle.Emit(1);
        }

        protected void CreateAndSetBullet(Transform firePoint)
        {
            var bullet = _bulletFactory.CreateBullet();
            bullet.SetPositionAndRotation(firePoint);
            _bullets.Add(bullet);
        }
        private void UpdateBullets()
        {
            if (!AnyBulletsSpawned)
            {
                return;
            }

            for (int i = 0; i < _bullets.Count; i++)
            {
                var bullet = _bullets[i];
                bullet.Move(settings.CurrentRange, out var bulletTrs);

                if (NonAllocBulletRaycasts(bulletTrs))
                {
                    HitTarget();
                    bullet.OnHit();
                    _bullets.Remove(bullet);
                    continue;
                }
                if (bullet.CheckIfBulletReachedEnd())
                {
                    _bullets.Remove(bullet);
                }
            }
        }
        private void HitTarget()
        {
            var hit = _bulletHitResults[0];
            var hittable = hit.transform.GetComponent<IHit>();
            hittable?.Hit(settings.CurrentDamage);
        }
        private void UpdateCrosshair()
        {
            if (NonAllocCrosshairRaycast())
            {
                SetCrosshairPosition(_crosshairHitResults[0].point);
            }
            else
            {
                SetCrosshairPosition(CrosshairDefaultPosition);
            }
        }
        public void FlipWeapon(bool isUp)
        {
            if (isUp)
            {
                transform.localScale = Vector3.one.With(y: 1);
            }
            else
            {
                transform.localScale = Vector3.one.With(y: -1);
            }
        }
        private void SetCrosshairPosition(Vector3 position)
        {
            _crosshair.position = position;
        }
        private bool NonAllocBulletRaycasts(Transform bulletTrs)
        {
            return _nonAllocRaycaster.Raycast(bulletTrs.position, bulletTrs.right, _bulletHitResults, 1f, _bulletHitLayer);
        }
        private bool NonAllocCrosshairRaycast()
        {
            return _nonAllocRaycaster.Raycast(_firePoint.position, _firePoint.right, _crosshairHitResults, settings.CurrentRange, _bulletHitLayer);
        }

        public void Dispose()
        {
            settings.ResetUpgrades();
            Destroy(gameObject);
        }


    }
}