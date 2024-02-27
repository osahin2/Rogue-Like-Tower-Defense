using Data;
using Inputs;
using Player.Weapons;
using SaveLoad;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IPlayer
    {
        [SerializeField] private MoveSettings _moveSettings;
        [SerializeField] private WeaponFactory _weaponFactory;
        [SerializeField] private Transform _weaponParent;

        [SerializeField] private SaveableData<PlayerData> _playerData;
        //[SerializeField] private SpellManager _spellController;

        private PlayerMovement _playerMovement;
        private IInputSystem _input;
        private IWeapon _currentWeapon;
        private IGameDataManager _saveLoadSystem;

        private bool _isPlayerActive;

        public Transform PlayerPosition => transform;
        private PlayerData PlayerData => _playerData.GetData();

        public void Init(IInputSystem inputSystem, IGameDataManager saveLoad)
        {
            _input = inputSystem;
            _saveLoadSystem = saveLoad;

            _playerData.Bind(_saveLoadSystem.GameData.PlayerData);

            _playerMovement = new PlayerMovement(_input, _moveSettings);

            _playerMovement.Init();
            _weaponFactory.Init();

            _weaponFactory.CreateWeapon(PlayerData.weaponType, _weaponParent);
            //_spellController.Init();

            _isPlayerActive = true;
        }

        private void Update()
        {
            if (!_isPlayerActive)
                return;

            _playerMovement.HandleMovement();
        }
    }

    [Serializable]
    public struct MoveSettings
    {
        public Transform MoveObject;
        public float DampenFactor;
        public float JoystickDeadZone;
    }
}