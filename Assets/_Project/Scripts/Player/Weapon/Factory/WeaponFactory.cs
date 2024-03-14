using System.Collections.Generic;
using UnityEngine;

namespace Player.Weapons
{
    public class WeaponFactory : MonoBehaviour, IWeaponFactory
    {
        [SerializeField] private List<WeaponConfig> _weapons = new();

        private readonly Dictionary<WeaponType, WeaponConfig> _weaponsMap = new();

        private Weapon _weapon;
        public void Init()
        {
            SetDictionary();
        }

        private void SetDictionary()
        {
            foreach (var weapon in _weapons)
            {
                weapon.Construct();
                _weaponsMap.Add(weapon.WeaponType, weapon);
            }
        }
        public IWeapon CreateWeapon(WeaponType type, Transform parent)
        {
            var weaponSettings = GetWeaponSetting(type);
            _weapon = Instantiate(weaponSettings.WeaponPrefab, parent);
            _weapon.Init(weaponSettings);
            return _weapon;
        }
        private WeaponConfig GetWeaponSetting(WeaponType type)
        {
            if (_weaponsMap.TryGetValue(type, out var weapon))
            {
                return weapon;
            }
            throw new KeyNotFoundException($"Weapon Factory: {type} Type Weapon Not Found");
        }
    }

}