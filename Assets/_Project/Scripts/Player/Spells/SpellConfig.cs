using GameObjectExtension;
using Player.Upgrades;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Spells
{
    [CreateAssetMenu(fileName ="Spell Config", menuName ="Spells/Spell Config")]
    public class SpellConfig : ScriptableObject
    {
        [SerializeField] private SpellType _spellType;
        [SerializeField] private Spell _spellPrefab;
        [SerializeField] private UpgradeableAttribute<int> _damageAttributes;
        [SerializeField] private UpgradeableAttribute<float> _rangeAttributes;
        [SerializeField] private UpgradeableAttribute<float> _cooldownAttributes;

        private IUpgrade<int> _damageUpgrade;
        private IUpgrade<float> _rangeUpgrade;
        private IUpgrade<float> _cooldownUpgrade;

        public SpellType SpellType => _spellType;
        public int Damage => _damageUpgrade.Current;
        public float Range => _rangeUpgrade.Current;
        public float Cooldown => _cooldownUpgrade.Current;

        private Spell _cachedSpell;

        public void Construct()
        {
            ConstructUpgrades();

        }
        public void ResetUpgrades()
        {
            _damageUpgrade.Reset();
            _rangeUpgrade.Reset();
            _cooldownUpgrade.Reset();
        }
        private void ConstructUpgrades()
        {
            _damageUpgrade = new AttributeUpgrade<int>(
                _damageAttributes.Attributes,
                UpgradeType.Spell,
                UpgradeAttributeType.Damage);

            _rangeUpgrade = new AttributeUpgrade<float>(
                _rangeAttributes.Attributes,
                UpgradeType.Spell,
                UpgradeAttributeType.Damage);

            _cooldownUpgrade = new AttributeUpgrade<float>(
                _cooldownAttributes.Attributes,
                UpgradeType.Spell,
                UpgradeAttributeType.Damage);
        }

        public Spell CreateOrGet(Transform parent)
        {
            if (!_cachedSpell.IsNull())
            {
                return _cachedSpell;
            }
            
            _cachedSpell = Instantiate(_spellPrefab, parent);
            return _cachedSpell;
        }

    }


}