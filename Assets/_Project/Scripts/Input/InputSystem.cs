using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inputs
{
    public class InputSystem : MonoBehaviour, IInputSystem
    {
        public event EventHandler<PointerEventData> OnPointerDowned;
        public event EventHandler<PointerEventData> OnPointerUpped;
        public event EventHandler<PointerEventData> OnPointerDragged;

        [SerializeField] private Joystick _joystick;

        public float Horizontal => _joystick.Horizontal;
        public float Vertical => _joystick.Vertical;   

        private void OnEnable()
        {
            _joystick.PointerDown += OnPointerDownHandler;
            _joystick.PointerUp += PointerUpHandler;
            _joystick.PointerDrag += PointerDragHandler;
        }

        private void OnDisable()
        {
            _joystick.PointerDown -= OnPointerDownHandler;
            _joystick.PointerUp -= PointerUpHandler;
            _joystick.PointerDrag -= PointerDragHandler;
        }


        private void OnPointerDownHandler(object sender, PointerEventData e)
        {
            OnPointerDowned?.Invoke(this, e);
        }
        private void PointerUpHandler(object sender, PointerEventData e)
        {
            OnPointerUpped?.Invoke(this, e);
        }
        private void PointerDragHandler(object sender, PointerEventData e)
        {
            OnPointerDragged?.Invoke(this, e);
        }

    }
}