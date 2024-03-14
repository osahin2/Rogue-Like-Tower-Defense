using UnityEngine;

namespace Player.Weapons
{
    public interface IWeapon
    {
        public Vector3 LeftHandPosition { get; }
        public Vector3 RightHandPosition { get; }
        void UpdateWeapon();
        void FlipWeapon(bool isUp);
        void Dispose();
    }

}