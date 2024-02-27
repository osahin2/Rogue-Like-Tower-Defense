using DG.Tweening;
using Inputs;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerMovement
    {
        private IInputSystem _input;
        private MoveSettings _settings;

        private bool _isMovementActive;
        private float _targetAngle;
        private Tween _rotateTween;

        private float GetDampen => 1.0f - Mathf.Exp(-_settings.DampenFactor * Time.deltaTime);
        private Transform MoveObject => _settings.MoveObject;
        private bool CheckDeadZone => Mathf.Abs(_input.Horizontal) <= _settings.JoystickDeadZone &&
            Mathf.Abs(_input.Vertical) <= _settings.JoystickDeadZone;

        public PlayerMovement(IInputSystem input, MoveSettings settings)
        {
            _input = input;
            _settings = settings;
        }

        public void Init()
        {
            AddEvents();
        }

        public void DeInit()
        {
            RemoveEvents();
        }

        public void HandleMovement()
        {
            if (!_isMovementActive || CheckDeadZone)
                return;

            var direction = new Vector3(_input.Horizontal, _input.Vertical, 0f);
            _targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var targetRot = Quaternion.Euler(0f, 0f, _targetAngle);
            MoveObject.rotation = Quaternion.Slerp(MoveObject.rotation, targetRot, GetDampen);
        }
        private void EndMovementTween()
        {
            _rotateTween = MoveObject.DORotateQuaternion(Quaternion.Euler(0f, 0f, _targetAngle), .15f);
        }

        private void OnPointerDownHandler(object sender, PointerEventData e)
        {
            _isMovementActive = true;
            _rotateTween.Kill(true);
        }
        private void OnPointerUpHandler(object sender, PointerEventData e)
        {
            _isMovementActive = false;
            EndMovementTween();
        }

        private void AddEvents()
        {
            _input.OnPointerDowned += OnPointerDownHandler;
            _input.OnPointerUpped += OnPointerUpHandler;
        }

        private void RemoveEvents()
        {
            _input.OnPointerDowned -= OnPointerDownHandler;
            _input.OnPointerUpped -= OnPointerUpHandler;
        }
    }
}