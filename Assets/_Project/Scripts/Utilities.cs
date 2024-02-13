using UnityEngine;

namespace Utility
{
    public static class Utilities
    {
        public static Vector3 RandomPointOnCircle(Vector2 centerPoint, float radius)
        {
            var randomPointOnCircle = centerPoint + (Random.insideUnitCircle * radius);
            return new Vector3(randomPointOnCircle.x, 0, randomPointOnCircle.y);
        }
    }
}