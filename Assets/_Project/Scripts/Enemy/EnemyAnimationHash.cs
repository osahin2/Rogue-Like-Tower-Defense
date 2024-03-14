using UnityEngine;

namespace Rogue_Enemy
{
    public static class EnemyAnimationHash
    {
        public static readonly int Walk = Animator.StringToHash("EnemyWalk");
        public static readonly int Roll = Animator.StringToHash("EnemyRoll");
        public static readonly int Hit = Animator.StringToHash("EnemyHit");
    }
}
