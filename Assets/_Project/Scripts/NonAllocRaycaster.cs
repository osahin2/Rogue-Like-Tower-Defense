using UnityEngine;

namespace Player.Weapons
{
    public class NonAllocRaycaster
    {
        public bool Raycast(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layer)
        {
            var hits = Physics2D.RaycastNonAlloc(
                origin,
                direction,
                results,
                distance,
                layer);
            if (hits == 0)
            {
                return false;
            }
            return true;
        }
    }
}