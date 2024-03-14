using UnityEngine;

namespace Rogue_Enemy
{
    [ComponentDependency(typeof(IAgent))]
    public class DefaultMovement : MonoBehaviour, IEnemyMovement
    {
        private IAgent _agent;
        public void Construct()
        {
            _agent = GetComponent<IAgent>();
            _agent.Construct();
        }
        public void Move(Transform target)
        {
            _agent.Move(target.position);
        }

        public void ResetMovement()
        {
            _agent.ResetAgent();
        }
    }
}
