using System.Collections.Generic;
using UnityEngine;

namespace Player.Weapons
{
    public class WeaponFactory : MonoBehaviour, IWeaponFactory
    {
        [SerializeField] private List<WeaponSettings> _weapons = new();

        private readonly Dictionary<WeaponType, WeaponSettings> _weaponsMap = new();
        public void Init()
        {
            SetDictionary();
        }

        private void SetDictionary()
        {
            foreach (var weapon in _weapons)
            {
                _weaponsMap.Add(weapon.WeaponType, weapon);
            }
        }
        public IWeapon CreateWeapon(WeaponType type)
        {
            var weaponSettings = GetWeaponSettings(type);
            var weapon = Instantiate(weaponSettings.WeaponPrefab);
            return weapon;
        }

        private WeaponSettings GetWeaponSettings(WeaponType type)
        {
            if (_weaponsMap.TryGetValue(type, out var weapon))
            {
                return weapon;
            }
            throw new KeyNotFoundException($"Weapon Factory: {type} Type Weapon Not Found");
        }
    }

}