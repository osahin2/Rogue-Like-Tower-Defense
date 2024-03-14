using System.Collections.Generic;

namespace Assets._Project.Scripts.UI.Inventory
{
    public class InventoryModel : SModel
    {
        public List<InventoryItemData> Items { get; private set; } = new();

        public InventoryModel(List<InventoryItemConfig> items = null)
        {
            foreach (var item in items)
            {
                Items.Add(item.CreateSaveData());
            }
        }

        public void Bind(InventorySaveData inventoryData)
        {
            bool isNew = inventoryData.Items == null || inventoryData.Items.Count == 0;
            if (isNew)
            {
                inventoryData.Items = new();
            }
            else
            {
                for (int i = 0; i < inventoryData.Items.Count; i++)
                {
                    inventoryData.Items[i].ItemConfig = InventoryItemDatabase.GetDataById(inventoryData.Id);
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