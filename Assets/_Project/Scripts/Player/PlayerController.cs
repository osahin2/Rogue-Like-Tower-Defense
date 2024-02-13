using Inputs;
using Service_Locator;
using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public interface IPlayer
    {
        public Transform PlayerPosition { get; }
    }
    public class PlayerController : MonoBehaviour, IPlayer
    {
        [SerializeField] private MoveSettings _moveSettings;

        private PlayerMovement _playerMovement;
        private IInputSystem _input;


        private bool _isPlayerActive;

        public Transform PlayerPosition => transform;

        public void Init(IInputSystem inputSystem)
        {
            _input = inputSystem;

            _playerMovement = new PlayerMovement(_input, _moveSettings);
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
        public Transform moveObject;
        public float dampenFactor;
    }
}