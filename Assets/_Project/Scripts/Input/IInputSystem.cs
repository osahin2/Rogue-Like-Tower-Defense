using System;
using UnityEngine.EventSystems;

namespace Inputs
{
    public interface IInputSystem
    {
        public event EventHandler<PointerEventData> OnPointerDowned;
        public event EventHandler<PointerEventData> OnPointerUpped;
        public event EventHandler<PointerEventData> OnPointerDragged;
        public float Horizontal { get; }
        public float Vertical { get; }

    }
}