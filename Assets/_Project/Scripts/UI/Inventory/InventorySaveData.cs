using SaveLoad;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.UI.Inventory
{
    [Serializable]
    public class InventorySaveData : ISaveData
    {
        [field: SerializeField] public string Id { get; set; }
        public List<InventoryItemData> Items;
    }
}