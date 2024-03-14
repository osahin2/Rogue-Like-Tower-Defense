using Data;
using Inputs;
using Player.Weapons;
using SaveLoad;
using System;
using UnityEngine;
using HealthSystem;
using SpriteFlip;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerController : MonoBehaviour, IPlayer
    {
        [SerializeField] private SpriteRenderer _bodySprite;
        [SerializeField] private Material _defaultMat;
        [Header("Health")]
        [SerializeField] private float _maxHealth;
        [Header("Move")]
        [SerializeField] private MoveSettings _moveSettings;
        [Header("Weapon")]
        [SerializeField] private WeaponFactory _weaponFactory;
        [SerializeField] private Transform _weaponParent;
        [Header("Hands")]
        [SerializeField] private SpriteRenderer _leftHand;
        [SerializeField] private SpriteRenderer _rightHand;
        [Header("Saveable Data")]
        [SerializeField] private SaveableData<PlayerSaveData> _playerData;
        //[SerializeField] private SpellManager _spellController;

        private PlayerMovement _playerMovement;
        private IInputSystem _input;
        private IWeapon _currentWeapon;
        private IGameDataManager _gameDataManager;
        private IHealth _health;
        private IFlash _flashEffect;
        private SpriteFlipper _bodySpriteFlipper;
        private SpriteFlipper _leftHandFlipper;
        private SpriteFlipper _rightHandFlipper;

        public Transform PlayerPosition => transform;
        private PlayerSaveData PlayerData => _playerData.GetData();

        private bool _isPlayerActive;

        public void Construct(IInputSystem inputSystem, IGameDataManager gameDataManager)
        {
            _input = inputSystem;
            _gameDataManager = gameDataManager;

            _playerData.Bind(_gameDataManager.GameData.PlayerData);

            _playerMovement = new PlayerMovement(_input, _moveSettings);
            _health = new Health(_maxHealth);
            _flashEffect = _bodySprite.GetComponent<IFlash>();
            _flashEffect?.Construct(_bodySprite, _defaultMat);

            ConstructSpriteFlippers();
        }
        public void Init()
        {
            _playerMovement.Init();
            _weaponFactory.Init();

            _currentWeapon = _weaponFactory.CreateWeapon(PlayerData.weaponType, _weaponParent);
            SetHandPositions();

            AddEvents();
            _isPlayerActive = true;
        }
        public void DeInit()
        {
            RemoveEvents();
            _isPlayerActive = false;
            _currentWeapon = null;
        }

        private void ConstructSpriteFlippers()
        {
            _bodySpriteFlipper = new SpriteFlipper(_bodySprite);
            _leftHandFlipper = new SpriteFlipper(_leftHand);
            _rightHandFlipper = new SpriteFlipper(_rightHand);
        }
        private void Update()
        {
            if (!_isPlayerActive)
                return;

            _playerMovement.HandleMovement();
            _currentWeapon.UpdateWeapon();
            SetHandPositions();
        }
        private void SetHandPositions()
        {
            _leftHand.transform.position = _currentWeapon.LeftHandPosition;
            _rightHand.transform.position = _currentWeapon.RightHandPosition;
        }
        public void Hit(float damage)
        {
            _flashEffect?.Flash();
            _health.Decrease(damage, OnHealthReducedToZero);
        }
        private void FlipSpritesToDirection()
        {
            if (Mathf.Abs(_input.Horizontal) < _moveSettings.JoystickDeadZone)
            {
                return;
            }

            _bodySpriteFlipper.FlipTowardOnX(_input.Horizontal);
            if (_input.Horizontal > 0)
            {
                _leftHandFlipper.FlipSprite(SpriteFlipDirection.Up);
                _rightHandFlipper.FlipSprite(SpriteFlipDirection.Up);
                _currentWeapon.FlipWeapon(isUp: true);
            }
            else
            {
                _leftHandFlipper.FlipSprite(SpriteFlipDirection.Down);
                _rightHandFlipper.FlipSprite(SpriteFlipDirection.Down);
                _currentWeapon.FlipWeapon(isUp: false);
            }
        }
        private void OnHealthReducedToZero()
        {
            Debug.Log("Player Health Reduced Zero");
        }
        private void OnDragged(object sender, PointerEventData data)
        {
            FlipSpritesToDirection();
        }
        private void AddEvents()
        {
            _input.OnPointerDragged += OnDragged;
        }
        private void RemoveEvents()
        {
            _input.OnPointerDragged -= OnDragged;

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