using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.UI.Inventory
{
    public abstract class InventoryItemConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; set; } = Guid.NewGuid().ToString();
        [SerializeField] private InventoryItemUI _itemPrefab;
        public InventoryItemType ItemType;
        public int ConfigTypeAsEnum;
        public Sprite Icon { get; protected set; }
        public Dictionary<string, string> Stats { get; protected set; } = new();

        public abstract void Construct();

        public InventoryItemData CreateSaveData()
        {
            return new InventoryItemData(this);
        }
        public InventoryItemUI CreateItemUI(Transform parent)
        {
            var itemUI = Instantiate(_itemPrefab);
            itemUI.Init(this);
            return itemUI;
        }
    }
}