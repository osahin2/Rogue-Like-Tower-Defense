using Player.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.UI.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Weapon Item", menuName = "UI/Inventory/Inventory Weapon Item")]
    public class InventoryWeaponTypeConfig : InventoryItemConfig
    {
        [SerializeField] private WeaponConfig _weapon;

        public override void Construct()
        {
            ConfigTypeAsEnum = (int) _weapon.WeaponType;
            Icon = _weapon.Icon;
            Stats = new Dictionary<string, string>()
                {
                    {"Damage", $"{_weapon.DefaultDamage}" },
                    {"Range", $"{_weapon.DefaultRange}"},
                    {"Fire Rate", $"{_weapon.DefaultFireRate}" }
                };
        }
    }
}