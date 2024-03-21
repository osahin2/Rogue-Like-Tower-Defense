using Assets._Project.Scripts.UI.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public interface IInventoryData
    {
        List<InventoryItemConfig> GetInventoryItems();
        InventoryItemConfig GetInventoryItemById(string id);
    }
    [Serializable]
    public class InventoryDatabase : IInventoryData
    {
        [SerializeField] private List<InventoryItemConfig> _configs = new();

        private readonly Dictionary<string, InventoryItemConfig> _itemDataDictionary = new();
        public void Construct()
        {
            ConstructConfigsAndSetDictionary();
        }

        private void ConstructConfigsAndSetDictionary()
        {
            foreach (var config in _configs)
            {
                config.Construct();
                _itemDataDictionary.Add(config.Id, config);
            }
        }
        public InventoryItemConfig GetInventoryItemById(string id)
        {
            if (!_itemDataDictionary.TryGetValue(id, out var data))
            {
                throw new KeyNotFoundException($"InventoryDataBase: Cannot find item data with id {id}");
            }
            return data;
        }

        public List<InventoryItemConfig> GetInventoryItems()
        {
            return _configs;
        }
    }
}