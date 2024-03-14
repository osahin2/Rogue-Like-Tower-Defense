using UnityEngine;

namespace Rogue_Enemy
{
    public interface IAgent
    {
        void Construct();
        void Move(Vector3 target);
        void ResetAgent();
    }
}
