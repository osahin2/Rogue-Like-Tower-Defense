using UnityEngine;

namespace Rogue_Enemy
{
    [CreateAssetMenu(fileName = "Enemy Movement Config", menuName = "Enemy/Movement Config")]
    public class EnemyMovementConfig : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _stoppingDistance;

        public float Speed => _speed;
        public float StoppingDistance => _stoppingDistance;

    }
}
