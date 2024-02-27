using System;
using System.Collections.Generic;
namespace Player.Upgrades
{
    public class UpgradeHandler<TUpgrade>
    {
        private List<IUpgrade<TUpgrade>> _upgrades = new();

        private readonly Dictionary<UpgradeAttributeType, IUpgrade<TUpgrade>> _upgradeDict = new();
        public UpgradeHandler(List<IUpgrade<TUpgrade>> upgrades)
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
        private bool TryGetUpgrade(UpgradeAttributeType type, out IUpgrade<TUpgrade> upgrade)
        {
            if (_upgradeDict.TryGetValue(type, out upgrade))
            {
                return true;
            }
            return false;
        }
        public TUpgrade GetAttribute(UpgradeAttributeType type)
        {
            if (TryGetUpgrade(type, out var upgrade))
            {
                return upgrade.Current;
            }
            throw new KeyNotFoundException($"{type} UpgradeController: {type} Upgrade Not Found In Dictionary");
        }
    }
}