using Player.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.UI.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Weapon Item", menuName = "UI/Inventory/Inventory Weapon Item")]
    public class InventoryWeaponTypeConfig : InventoryItemConfig
    {
        [SerializeField] private WeaponConfig _weapon;
        [SerializeField] private List<ItemProperty> _itemProperties = new();
        public override void Construct()
        {
            ConfigTypeAsEnum = (int) _weapon.WeaponType;
            ItemName = _weapon.WeaponType.ToString();
            _spriteSettings.Sprite = _weapon.Icon;
            foreach (var item in _itemProperties)
            {
                item.Value = GetValue(item.Type);
                Stats.Add(item);
            }
        }

        private string GetValue(PropertyType type) => type switch
        {
            PropertyType.Damage => _weapon.DefaultDamage.ToString(),
            PropertyType.FireRate => _weapon.DefaultFireRate.ToString(),
            PropertyType.Range => _weapon.DefaultRange.ToString()
        };
    }
}