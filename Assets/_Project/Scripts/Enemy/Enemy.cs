using CountdownTimer;
using Cysharp.Threading.Tasks;
using HealthSystem;
using Player.Weapons;
using System;
using UnityEngine;
using SpriteFlip;
using Pool;

namespace Rogue_Enemy
{
    [ComponentDependency(typeof(IEnemyMovement))]
    public class Enemy : MonoBehaviour, IEnemy
    {
        public event Action<IEnemy> OnDead;
        private Action<Enemy> SetFreeAction;

        [Header("Animation")]
        [SerializeField] private AnimatorController _animatorController;
        [Header("Enemy")]
        [SerializeField] protected EnemyType _enemyType;
        [SerializeField] private SpriteRenderer _enemySprite;
        [SerializeField] private Material _enemyDefaultMat;
        [Header("Health")]
        [SerializeField] private float _maxHealth;
        [Header("Attack")]
        [SerializeField] private float _damage;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackCooldown;

        private IEnemyMovement _enemyMovement;
        private IHealth _health;
        private IFlash _flashEffect;
        private ICountdown _attackTimer;
        private SpriteFlipper _spriteFlipper;
        private NonAllocRaycaster _nonAllocRaycaster;

        public EnemyType EnemyType => _enemyType;
        private Vector3 DirectionToTarget => _target - transform.position;

        private Vector3 _target;
        private RaycastHit2D[] _attackRayResults = new RaycastHit2D[1];
        private int _playerLayer;

        public void Construct(Action<Enemy> setFree)
        {
            SetFreeAction = setFree;

            _enemyMovement = GetComponent<IEnemyMovement>();
            _enemyMovement.Construct();

            _health = new Health(_maxHealth);
            _attackTimer = new Countdown();
            _nonAllocRaycaster = new NonAllocRaycaster();
            _spriteFlipper = new SpriteFlipper(_enemySprite);

            _animatorController.Init();

            _flashEffect = _enemySprite.GetComponent<IFlash>();
            _flashEffect?.Construct(_enemySprite, _enemyDefaultMat);

            _playerLayer = LayerMask.GetMask("Player");
        }
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        public void Move(Transform target)
        {
            _target = target.position;

            _enemyMovement.Move(target);
            _spriteFlipper.FlipTowardOnX(DirectionToTarget.x);
        }
        public void Attack()
        {
            if (!_nonAllocRaycaster.Raycast(transform.position, DirectionToTarget, _attackRayResults, _attackRange, _playerLayer))
            {
                return;
            }

            if (_attackTimer.Check(Time.deltaTime, _attackCooldown))
            {
                OnAttack();
            }
        }
        private void OnAttack()
        {
            var hitTarget = _attackRayResults[0];
            var hittable = hitTarget.transform.GetComponent<IHit>();
            hittable?.Hit(_damage);
        }
        public async void Hit(float damage)
        {
            _flashEffect?.Flash();

            _health.Decrease(damage, OnHealthReducedToZero);

            await UniTask.WaitForSeconds(_animatorController.PlayAnimation(EnemyAnimationHash.Hit));
            _animatorController.PlayAnimation(EnemyAnimationHash.Walk);
        }

        private void OnHealthReducedToZero()
        {
            _animatorController.SetActiveAnimator(false);
            _enemyMovement.ResetMovement();

            OnDead?.Invoke(this);
            SetFreeAction(this);
        }
        public void PoolOnFree()
        {
            gameObject.SetActive(false);
        }
        public void PoolOnDestroyed()
        {
            Destroy(gameObject);
        }
        public virtual void PoolOnGet()
        {
            _animatorController.SetActiveAnimator(true);
            _health.Reset();
            gameObject.SetActive(true);
        }

        public void PoolInitialSpawned()
        {
            gameObject.SetActive(false);
        }
    }
}
