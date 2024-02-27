using Service_Locator;
using System.Collections.Generic;
using Player.Upgrades;
using UnityEngine;
using Player.Spells;
using System;

namespace Player.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon Settings")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private WeaponType _type;
        [SerializeField] private Weapon _weaponPrefab;
        [SerializeField] private UpgradeableAttribute<int> _damageAttributes;
        [SerializeField] private UpgradeableAttribute<float> _fireRateAttributes;
        [SerializeField] private UpgradeableAttribute<float> _rangeAttribute;

        private IUpgrade<int> _damageUpgrade;
        private IUpgrade<float> _fireRateUpgrade;
        private IUpgrade<float> _rangeUpgrade;

        public Weapon WeaponPrefab => _weaponPrefab;
        public WeaponType WeaponType => _type;
        public int GetDamage => _damageUpgrade.Current;
        public float GetFireRate => _fireRateUpgrade.Current;
        public float GetRange => _rangeUpgrade.Current;

        public void Construct()
        {
            _damageUpgrade = new AttributeUpgrade<int>(
                _damageAttributes.Attributes, 
                UpgradeType.Weapon, 
                UpgradeAttributeType.Damage);

            _fireRateUpgrade = new AttributeUpgrade<float>(
                _fireRateAttributes.Attributes, 
                UpgradeType.Weapon, 
                UpgradeAttributeType.FireRate);

            _rangeUpgrade = new AttributeUpgrade<float>(
                _rangeAttribute.Attributes, 
                UpgradeType.Weapon, 
                UpgradeAttributeType.Range);

        }

        public void ResetUpgrades()
        {
            _damageUpgrade.Reset();
            _fireRateUpgrade.Reset();
            _rangeUpgrade.Reset();
        }
    }
}