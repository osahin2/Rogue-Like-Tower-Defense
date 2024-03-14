using UnityEngine;

namespace Player
{
    public interface IPlayer : IHit
    {
        public Transform PlayerPosition { get; }
    }
}