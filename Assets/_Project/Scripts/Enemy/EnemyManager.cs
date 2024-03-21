using Cysharp.Threading.Tasks;
using Data;
using Player;
using Rogue_Enemy.Factory;
using Rogue_LevelData;
using Service_Locator;
using System.Collections.Generic;
using UnityEngine;

namespace Rogue_Enemy
{
    public class EnemyManager : MonoBehaviour, IEnemyManager
    {
        [Header("Enemy Factories")]
        [SerializeField] private List<GenericEnemyFactory> _enemyFactories = new();
        [Header("Spawn Settings")]
        [SerializeField] private float _spawnRadius;
        [SerializeField] private LevelDataContainer _levelDataContainer;

        private IEnemyFactory _enemyFactory;
        private IPlayer _player;
        private LevelData _currentLevelData;
        private IGameDataManager _gameData;

        public IEnemyFactory EnemyFactory => _enemyFactory ??= new EnemyFactory(_enemyFactories);
        public int SpawnedEnemyCount => _enemies.Count;
        public float SpawnRadius => _spawnRadius;
        private LevelSaveData LevelData => _gameData.GameData.LevelData;

        private List<IEnemy> _enemies = new();
        private bool _isActive;

        public void Construct(IPlayer player)
        {
            _player = player;
            ServiceProvider.Instance.Get(out _gameData);

            _enemyFactory ??= new EnemyFactory(_enemyFactories);
            _enemyFactory.Init();
            _levelDataContainer.Construct();
        }

        public void Init()
        {
            _isActive = true;
            _levelDataContainer.GetLevelData(1, out _currentLevelData);
            IterateWavesAsync();
        }
        public void DeInit()
        {
            _isActive = false;
        }

        private void Update()
        {
            if (!_isActive) return;

            MoveEnemies();
            AttackEnemies();
        }
        private void MoveEnemies()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                var enemy = _enemies[i];
                enemy.Move(_player.PlayerPosition);
            }
        }
        private void AttackEnemies()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                var enemy = _enemies[i];
                enemy.Attack();
            }
        }
        private async void IterateWavesAsync()
        {
            foreach (var currentWave in _currentLevelData.Waves)
            {
                await SpawnEnemiesAsync(currentWave);
            }
            Debug.Log("Level Is Completed");
        }
        private async UniTask SpawnEnemiesAsync(WaveData currentWave)
        {
            List<UniTask> spawnTasks = new();
            foreach (var waveEnemy in currentWave.WaveEnemies)
            {
                spawnTasks.Add(SpawnWaveEnemyAsync(waveEnemy));
            }
            await UniTask.WhenAll(spawnTasks);
        }


        private async UniTask SpawnWaveEnemyAsync(WaveEnemyData waveEnemy)
        {
            for (int i = 0; i < waveEnemy.TotalCount; i++)
            {
                var enemy = _enemyFactory.Get(waveEnemy.EnemyType);
                enemy.SetPosition(RandomPointOnCircleEdge());
                enemy.OnDead += OnEnemyDead;
                _enemies.Add(enemy);
                await UniTask.WaitForSeconds(GetSpawnInterval(waveEnemy.TotalCount, _currentLevelData.WaveDuration));
            }
        }

        private void OnEnemyDead(IEnemy enemy)
        {
            enemy.OnDead -= OnEnemyDead;
            _enemies.Remove(enemy);
        }
        private Vector3 RandomPointOnCircleEdge()
        {
            Vector2 centerPoint = _player.PlayerPosition.position;
            var randomPointOnCircle = centerPoint + (Random.insideUnitCircle.normalized * _spawnRadius);
            return randomPointOnCircle;
        }
        private float GetSpawnInterval(int totalEnemyCount, float waveDuration)
        {
            return waveDuration / totalEnemyCount;
        }

    }
}