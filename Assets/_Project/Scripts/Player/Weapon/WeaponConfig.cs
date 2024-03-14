using Player.Upgrades;
using Player.Weapons.Bullets;
using UnityEngine;

namespace Player.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon Config")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private WeaponType _type;
        [SerializeField] private Weapon _weaponPrefab;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private UpgradeableAttribute<int> _damageAttributes;
        [SerializeField] private UpgradeableAttribute<float> _fireRateAttributes;
        [SerializeField] private UpgradeableAttribute<float> _rangeAttribute;
        [SerializeField] private Sprite _icon;

        private IUpgrade<int> _damageUpgrade;
        private IUpgrade<float> _fireRateUpgrade;
        private IUpgrade<float> _rangeUpgrade;

        public Weapon WeaponPrefab => _weaponPrefab;
        public BulletConfig BulletConfig => _bulletConfig;
        public WeaponType WeaponType => _type;
        public Sprite Icon => _icon;
        public int CurrentDamage => _damageUpgrade.Current;
        public float CurrentFireRate => _fireRateUpgrade.Current;
        public float CurrentRange => _rangeUpgrade.Current;

        public int DefaultDamage => _damageAttributes.Attributes[0];
        public float DefaultFireRate => _fireRateAttributes.Attributes[0];
        public float DefaultRange => _rangeAttribute.Attributes[0];

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