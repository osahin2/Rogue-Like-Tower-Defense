using Data;
using System;
using System.Collections.Generic;

namespace Assets._Project.Scripts.UI.Inventory
{
    public class InventoryPresenter : SPresenter<InventoryModel, InventoryView>
    {
        public event Action<InventoryItemConfig> OnItemEquipEvent;

        private InventoryItemConfig _currentClickedConfig;
        private InventoryPresenter(InventoryModel model, InventoryView view) : base(model, view)
        {
            
        }

        protected override void OnInit()
        {
            //foreach (var item in _model.GetEquippedItems())
            //{
            //    _view.EquipItem(item.ItemConfig);
            //}
        }
        protected override void OnDeInit()
        {

        }
        protected override void OnShow()
        {
            
        }
        protected override void OnHide()
        {

        }

        public void Bind(InventorySaveData saveData, IInventoryData inventoryDatabase)
        {
            _model.Bind(saveData, inventoryDatabase);
        }
        private void OnItemClicked(InventoryItemConfig config)
        {
            _currentClickedConfig = config;
            _view.ShowItemDetail(config);
        }
        private void OnItemDetailClosed()
        {
            _currentClickedConfig = null;
            _view.HideItemDetail();
        }

        private void OnItemEquipped()
        {
            _model.SetItemEquipped(_currentClickedConfig.Id);
            _view.EquipItem(_currentClickedConfig);
            OnItemEquipEvent?.Invoke(_currentClickedConfig);
        }
        protected override void OnAddEvents()
        {
            _view.AddItemClickListener(OnItemClicked);
            _view.AddDetailsCloseListener(OnItemDetailClosed);
            _view.AddItemEquipListener(OnItemEquipped);
        }


        protected override void OnRemoveEvents()
        {
            _view.RemoveItemClickListeners(OnItemClicked);
            _view.RemoveDetailsCloseListener(OnItemDetailClosed);
            _view.RemoveItemEquipListener(OnItemEquipped);
        }

        public class Builder
        {
            private InventoryView _view;
            private List<InventoryItemConfig> _items;
            private List<InventoryItemConfig> _equippedItems;

            public Builder(InventoryView view)
            {
                _view = view;
            }
            public Builder WithStartingItems(List<InventoryItemConfig> items)
            {
                _items = items;
                return this;

            }
            public Builder WithEquippedItems(List<InventoryItemConfig> items)
            {
                _equippedItems = items;
                return this;
            }
            public InventoryPresenter Build()
            {
                var model = _items != null ? new InventoryModel(_items) : new InventoryModel();

                if (_equippedItems != null)
                {
                    foreach (var item in _equippedItems)
                    {
                        item.IsEquipped = true;
                    }
                }

                foreach (var item in model.Items)
                {
                    _view.SetInventoryItems(item.ItemConfig);
                }

                _view.SetItemSlotDictionary();

                return new InventoryPresenter(model, _view);
            }
        }
    }
}