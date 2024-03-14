using System.Collections.Generic;
using UnityEngine;
namespace Player.Weapons
{
    public class Shotgun : Weapon
    {
        [SerializeField] private List<Transform> _additionalFirePoints;

        protected override void OnFire()
        {
            CreateAndSetBullet(_firePoint);

            foreach (var firePoint in _additionalFirePoints)
            {
                CreateAndSetBullet(firePoint);
            }

            base.OnFire();
        }
    }

}