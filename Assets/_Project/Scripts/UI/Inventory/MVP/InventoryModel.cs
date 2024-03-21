using Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Assets._Project.Scripts.UI.Inventory
{
    public class InventoryModel : SModel
    {
        public List<InventoryItemData> Items { get; private set; } = new();

        private Dictionary<string, InventoryItemData> _itemsDictionary = new();

        private IInventoryData _inventoryDatabase;
        public InventoryModel(List<InventoryItemConfig> items = null)
        {
            foreach (var item in items)
            {
                Items.Add(item.CreateSaveData());
            }
        }

        public void Bind(InventorySaveData inventoryData, IInventoryData inventoryDatabase)
        {
            _inventoryDatabase = inventoryDatabase;

            bool isNew = inventoryData.Items == null || inventoryData.Items.Count == 0;
            if (isNew)
            {
                inventoryData.Items = new();
            }
            else
            {
                for (int i = 0; i < inventoryData.Items.Count; i++)
                {
                    inventoryData.Items[i].ItemConfig = _inventoryDatabase.GetInventoryItemById(Items[i].Id);
                }
            }

            if (isNew && Items.Count != 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    inventoryData.Items[i] = Items[i];
                }
            }

            Items = inventoryData.Items;
            SetItemsToDictionary();
        }
        private void SetItemsToDictionary()
        {
            foreach (var item in Items)
            {
                _itemsDictionary.Add(item.ItemConfig.Id, item);
            }
        }
        public List<InventoryItemData> GetEquippedItems()
        {
            return Items.Where(x => x.IsEquipped).ToList();
        }
        public void SetItemEquipped(string id)
        {
            if (!_itemsDictionary.TryGetValue(id, out var item))
            {
                throw new KeyNotFoundException($"InventoryModel: Cannot find item with id {id}");
            }

            var equippedOne = Items.FirstOrDefault(x => x.ItemConfig.ItemType == item.ItemConfig.ItemType);
            if (equippedOne != null)
            {
                equippedOne.IsEquipped = false;
            }
            item.IsEquipped = true;

        }
        public void Add(InventoryItemData itemData)
        {
            Items.Add(itemData);
        }
        protected override void OnInit()
        {

        }
        protected override void OnDeInit()
        {

        }

    }
}