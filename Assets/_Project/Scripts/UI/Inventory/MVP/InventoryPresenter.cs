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

        public void Bind(InventorySaveData saveData)
        {
            _model.Bind(saveData);
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

            public Builder(InventoryView view)
            {
                _view = view;
            }
            public Builder WithStartingItems(List<InventoryItemConfig> items)
            {
                _items = items;
                return this;

            }
            public InventoryPresenter Build()
            {
                var model = _items != null ? new InventoryModel(_items) : new InventoryModel();

                foreach (var item in model.Items)
                {
                    _view.SetInventoryItems(item.ItemConfig);
                }

                return new InventoryPresenter(model, _view);
            }
        }
    }
}