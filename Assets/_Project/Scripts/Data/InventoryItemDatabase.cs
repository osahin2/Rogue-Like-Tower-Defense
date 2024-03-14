using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.UI.Inventory
{
    public static class InventoryItemDatabase
    {
        private static Dictionary<string, InventoryItemConfig> _itemDataDictionary;

        public static List<InventoryItemConfig> ItemDataList { get; private set; } = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            _itemDataDictionary = new Dictionary<string, InventoryItemConfig>();

            var itemDatas = Resources.LoadAll<InventoryItemConfig>("");
            ItemDataList = ItemDataList;

            foreach (var item in itemDatas)
            {
                item.Construct();
                _itemDataDictionary.Add(item.Id, item);
            }
        }
        public static InventoryItemConfig GetDataById(string id)
        {
            try
            {
                return _itemDataDictionary[id];
            }
            catch
            {
                Debug.LogError($"Cannot find item data with id {id}");
                return null;
            }
        }
    }
}