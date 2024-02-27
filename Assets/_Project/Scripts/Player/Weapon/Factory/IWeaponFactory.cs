using UnityEngine;

namespace Player.Weapons
{
    public interface IWeaponFactory
    {
        IWeapon CreateWeapon(WeaponType type, Transform parent);
    }

}