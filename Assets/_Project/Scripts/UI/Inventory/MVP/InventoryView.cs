using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.UI.Inventory
{
    public class InventoryView : SView
    {
        [SerializeField] private InventoryItemUI _inventoryItemPrefab;
        [SerializeField] private Transform _inventoryItemsParent;
        [SerializeField] private ItemDetailUI _itemDetailUI;
        [SerializeField] private List<ItemEquipSlot> _itemSlots;

        public List<InventoryItemUI> InventoryItems { get; } = new();

        private readonly Dictionary<InventoryItemType, ItemEquipSlot> _itemEquipSlots = new();

        protected override void OnInit()
        {

        }
        protected override void OnDeInit()
        {

        }
        protected override void OnShow()
        {
            gameObject.SetActive(true);
        }
        protected override void OnHide()
        {
            gameObject.SetActive(false);
        }

        public void SetInventoryItems(InventoryItemConfig config)
        {
            var inventoryUI = Instantiate(_inventoryItemPrefab, _inventoryItemsParent);
            inventoryUI.Init(config);
            InventoryItems.Add(inventoryUI);

        }
        public void SetItemSlotDictionary()
        {
            foreach (var slot in _itemSlots)
            {
                _itemEquipSlots.Add(slot.Type, slot);
            }
        }

        public void EquipItem(InventoryItemConfig config)
        {
            if (!_itemEquipSlots.TryGetValue(config.ItemType, out var slot))
            {
                Debug.LogError("InventoryView: Item Equip Slot cannot find in Dictionary");
                return;
            }

            slot.SetItem(config);

        }
        public void ShowItemDetail(InventoryItemConfig config)
        {
            _itemDetailUI.Show(config);
        }
        public void HideItemDetail()
        {
            _itemDetailUI.Hide();
        }
        public void AddItemClickListener(Action<InventoryItemConfig> onClicked)
        {
            foreach (var item in InventoryItems)
            {
                item.OnItemClickEvent += onClicked;
            }
        }
        public void RemoveItemClickListeners(Action<InventoryItemConfig> onClicked)
        {
            foreach (var item in InventoryItems)
            {
                item.OnItemClickEvent -= onClicked;
            }
        }
        public void AddDetailsCloseListener(Action onClicked)
        {
            _itemDetailUI.OnCloseClickEvent += onClicked;
        }
        public void RemoveDetailsCloseListener(Action onClicked)
        {
            _itemDetailUI.OnCloseClickEvent -= onClicked;
        }
        public void AddItemEquipListener(Action onClicked)
        {
            _itemDetailUI.OnItemEquipEvent += onClicked;
        }
        public void RemoveItemEquipListener(Action onClicked)
        {
            _itemDetailUI.OnItemEquipEvent -= onClicked;
        }
        protected override void OnAddEvents()
        {

        }


        protected override void OnRemoveEvents()
        {

        }

    }
}