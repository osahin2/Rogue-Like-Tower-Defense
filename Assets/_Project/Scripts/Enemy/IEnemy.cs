using System;

namespace Rogue_Enemy
{
    public interface IEnemy : IHit
    {
        public event Action<Enemy> OnDead;
        void Attack();
    }
}
