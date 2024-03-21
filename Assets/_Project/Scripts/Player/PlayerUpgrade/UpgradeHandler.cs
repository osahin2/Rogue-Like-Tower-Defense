using System.Collections.Generic;
namespace Player.Upgrades
{
    public class UpgradeHandler
    {
        private List<IUpgrade> _upgrades = new();

        private readonly Dictionary<UpgradeAttributeType, IUpgrade> _upgradeDict = new();
        public UpgradeHandler(List<IUpgrade> upgrades)
        {
            _upgrades = upgrades;
            SetDictionary();
        }
        private void SetDictionary()
        {
            foreach (var upgrade in _upgrades)
            {
                _upgradeDict.Add(upgrade.AttributeType, upgrade);
            }
        }
        public IUpgrade GetUpgrade(UpgradeAttributeType type)
        {
            if (!_upgradeDict.TryGetValue(type, out var upgrade))
            {
                throw new KeyNotFoundException($"{type} UpgradeController: {type} Upgrade Not Found In Dictionary");
            }
            return upgrade;
        }

        public void UpgradeAttribute(UpgradeAttributeType type)
        {
            if (!_upgradeDict.TryGetValue(type, out var upgrade))
            {
                throw new KeyNotFoundException($"{type} UpgradeController: {type} Upgrade Not Found In Dictionary");
            }
            upgrade.SetLevel(upgrade.Level + 1);
        }
    }
}