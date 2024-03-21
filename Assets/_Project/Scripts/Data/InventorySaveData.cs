using Assets._Project.Scripts.UI.Inventory;
using SaveLoad;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class InventorySaveData : ISaveData
    {
        [field: SerializeField] public string Id { get; set; }
        public List<InventoryItemData> Items;
    }
}