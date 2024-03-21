using System;
using UnityEngine;

namespace Assets._Project.Scripts.UI.Inventory
{
    [Serializable]
    public class InventoryItemData
    {
        [field: SerializeField] public string Id { get; set; }
        public InventoryItemConfig ItemConfig;
        public string Name;
        public string ItemType;
        public bool IsEquipped;

        public InventoryItemData(InventoryItemConfig itemConfig)
        {
            ItemConfig = itemConfig;
            Name = ItemConfig.name;
            ItemType = ItemConfig.ItemType.ToString();
            Id = itemConfig.Id;
        }
    }
}