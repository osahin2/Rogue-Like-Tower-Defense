using System;
namespace Player.Upgrades
{
    public interface IUpgrade<TUpgrade>
    {
        UpgradeType Type { get; }
        UpgradeAttributeType AttributeType { get; }
        int Level { get; }
        bool IsMaxLevel { get; }
        TUpgrade Current { get; }
        void SetLevel(int level);
        void Reset();
    }
}