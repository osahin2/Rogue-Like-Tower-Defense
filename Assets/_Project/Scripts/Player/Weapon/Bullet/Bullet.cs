using System;
using UnityEngine;

namespace Player.Weapons.Bullets
{
    public class Bullet : MonoBehaviour, IBullet
    {
        private BulletConfig _bulletConfig;

        private Vector2 _velocity;
        private Vector3 _bulletTarget;

        private Action<Bullet> _onBulletReachedEnd;
        public void Construct(BulletConfig bulletConfig, Action<Bullet> onBulletReachedEnd)
        {
            _bulletConfig = bulletConfig;
            _onBulletReachedEnd = onBulletReachedEnd;
        }

        private float DampFactor => 1.0f - Mathf.Exp(-_bulletConfig.BulletDampen * Time.fixedDeltaTime);
        public void Move(float range, out Transform bulletTrs)
        {
            _bulletTarget = transform.right * range;
            transform.position =
                Vector2.SmoothDamp(transform.position, _bulletTarget, ref _velocity, DampFactor, _bulletConfig.BulletSpeed);

            bulletTrs = transform;
            if (CheckIfBulletReachedEnd())
            {
                OnBulletReachedEndInvoke();
            }
        }
        public bool CheckIfBulletReachedEnd() => Vector2.Distance(transform.position, _bulletTarget) <= 0.01f;
        public void OnGet()
        {
            gameObject.SetActive(true);
        }
        public void OnFree()
        {
            gameObject.SetActive(false);
        }
        public void OnDestroyed()
        {
            Destroy(gameObject);
        }
        public void InitiallySpawnedFromPooler()
        {
            gameObject.SetActive(false);
        }

        private void OnBulletReachedEndInvoke()
        {
            _onBulletReachedEnd?.Invoke(this);
        }

        public void OnHit()
        {
            OnBulletReachedEndInvoke();
        }

        public void SetPositionAndRotation(Transform trs)
        {
            transform.SetPositionAndRotation(trs.position, trs.rotation);
        }
    }
}