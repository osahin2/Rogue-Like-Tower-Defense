using System;
using UnityEngine;

namespace Player.Weapons.Bullets
{
    public interface IBullet
    {
        void SetPositionAndRotation(Transform trs);
        void Move(float range, out Transform transform);
        void OnHit();
        bool CheckIfBulletReachedEnd();
    }
}