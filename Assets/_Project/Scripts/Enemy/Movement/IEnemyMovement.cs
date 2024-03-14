using UnityEngine;

namespace Rogue_Enemy
{
    public interface IEnemyMovement
    {
        void Construct();
        void Move(Transform target);
        void ResetMovement();
    }
}
