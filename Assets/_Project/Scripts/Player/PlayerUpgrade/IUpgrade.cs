using System;
namespace Player.Upgrades
{
    public interface IUpgrade<TUpgrade> : IUpgrade
    {
        TUpgrade Current { get; }
    }
    public interface IUpgrade
    {
        UpgradeType Type { get; }
        UpgradeAttributeType AttributeType { get; }
        int Level { get; }
        bool IsMaxLevel { get; }
        void SetLevel(int level);
        void Reset();
    }
}