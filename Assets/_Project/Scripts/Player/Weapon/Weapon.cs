using UnityEngine;

namespace Player.Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public virtual void Attack()
        {

        }
    }

}