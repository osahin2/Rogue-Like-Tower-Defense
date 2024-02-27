using Data;
using Inputs;
using Player;
using Rogue_Enemy;
using Service_Locator;
using System;
using UnityEngine;

namespace App
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private GameDataManager _saveLoadManager;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private EnemyManager _enemyManager;

        public void Init()
        {
            _playerController.Init(_inputSystem, _saveLoadManager);
            _enemyManager.Init(_playerController);

            ServiceProvider.Instance.
                Register<IEnemyManager>(_enemyManager).
                Register<IInputSystem>(_inputSystem);
        }
    }

}
