using System;
using UnityEngine;

namespace Rogue_Enemy
{
    public interface IEnemy : IHit
    {
        public event Action<IEnemy> OnDead;
        void Attack();
        void Move(Transform target);
        void SetPosition(Vector3 position);
    }
}
