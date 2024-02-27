using System.Collections.Generic;
namespace Player.Upgrades
{
    public class AttributeUpgrade<TUpgrade> : IUpgrade<TUpgrade>
    {
        public UpgradeType Type => _upgradeType;
        public UpgradeAttributeType AttributeType => _attributeType;
        public int Level => _level;
        public bool IsMaxLevel => _level == _upgrades.Count;
        public TUpgrade Current => _upgrades[_level - 1];

        private List<TUpgrade> _upgrades = new();
        private UpgradeType _upgradeType;
        private UpgradeAttributeType _attributeType;
        private int _level = 1;

        public AttributeUpgrade(List<TUpgrade> upgrades, UpgradeType upgradeType, UpgradeAttributeType attributeType)
        {
            _upgrades = upgrades;
            _upgradeType = upgradeType;
            _attributeType = attributeType;
        }

        public void SetLevel(int level)
        {
            if (IsMaxLevel)
            {
                return;
            }
            _level = level;
        }

        public void Reset()
        {
            _level = 1;
        }
    }
}