using Data;
using Inputs;
using Player;
using Rogue_Enemy;
using Service_Locator;
using UIScripts;
using UnityEngine;

namespace App
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private GameDataManager _gameDataManager;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private UIManager _uiManager;
        public IGameDataManager GameDataManager => _gameDataManager;
        public UIManager UIManager => _uiManager;
        public PlayerController PlayerController => _playerController;
        public EnemyManager EnemyManager => _enemyManager;
        public void Construct()
        {
            RegisterInstances();

            _gameDataManager.Construct();
            _playerController.Construct(_inputSystem, _gameDataManager);
            _enemyManager.Construct(_playerController);
            _uiManager.Construct();

        }

        private void RegisterInstances()
        {
            ServiceProvider.Instance.
                Register<IInputSystem>(_inputSystem).
                Register<IGameDataManager>(_gameDataManager);
        }
    }

}
