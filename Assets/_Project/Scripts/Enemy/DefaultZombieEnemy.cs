using UnityEngine;

namespace Rogue_Enemy
{
    public class DefaultZombieEnemy : Enemy
    {
        public override int Damage => throw new System.NotImplementedException();
        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void Move(Transform target)
        {
            base.Move(target);
            //throw new System.NotImplementedException();

        }

    }
}
