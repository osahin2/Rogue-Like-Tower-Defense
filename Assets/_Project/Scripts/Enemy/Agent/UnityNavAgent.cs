using UnityEngine;
using UnityEngine.AI;
using Utility;

namespace Rogue_Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnityNavAgent : MonoBehaviour, IAgent
    {
        [SerializeField] private EnemyMovementConfig _enemyMovementConfig;
        private NavMeshAgent _agent;

        private bool _hasSetDestination;
        public void Construct()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _enemyMovementConfig.Speed;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }
        public void Move(Vector3 target)
        {
            if (Vector2.Distance(transform.position, target) <= _enemyMovementConfig.StoppingDistance)
            {
                _agent.isStopped = true;
                return;
            }
            if (_agent.isStopped)
            {
                _agent.isStopped = false;
            }
            SetDestination(target);
        }

        public void ResetAgent()
        {
            _agent.isStopped = false;
            _hasSetDestination = false;
        }

        private void SetDestination(Vector3 target)
        {
            if (_hasSetDestination)
            {
                return;
            }
            _agent.destination = target;
            _hasSetDestination = true;
        }
    }
}
