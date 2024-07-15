using Data;
using DG.Tweening;
using Player.Weapons;
using SaveLoad;
using Service_Locator;
using System;
using System.Collections.Generic;
using UIScripts;
using UnityEngine;

namespace Assets._Project.Scripts.UI.Inventory
{
    [Serializable]
    public enum InventoryItemType
    {
        Weapon
    }
    public class Inventory : MenuPanel
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private SaveableData<InventorySaveData> _inventorySaveData;
        [SerializeField] private List<InventoryItemConfig> _startingItems = new();
        [SerializeField] private List<InventoryItemConfig> _defaultEquippedItems = new();

        private InventoryPresenter _presenter;

        private InventorySaveData InventoryData => _inventorySaveData.GetData();
        private PlayerSaveData PlayerData => _gameDataManager.GameData.PlayerData;

        private IGameDataManager _gameDataManager;
        private IInventoryData _inventoryDatabase;

        public override void Construct()
        {
            ServiceProvider.Instance.
                Get(out _gameDataManager).
                Get(out _inventoryDatabase);

            _presenter = new InventoryPresenter.Builder(_inventoryView)
                .WithStartingItems(_startingItems)
                .WithEquippedItems(_defaultEquippedItems)
                .Build();

            BindSaveData();

            _presenter.Init();
        }

        public override void Show()
        {
            _presenter.Show();
            AddEvents();
        }

        public override void Hide()
        {
            RemoveEvents();
            _presenter.Hide();
        }
        private void OnItemEquipped(InventoryItemConfig config)
        {
            switch (config.ItemType)
            {
                case InventoryItemType.Weapon:
                    PlayerData.weaponType = (WeaponType)config.ConfigTypeAsEnum;
                    break;
            }
        }

        private void BindSaveData()
        {
            _inventorySaveData.Bind(_gameDataManager.GameData.InventoryData);
            _presenter.Bind(InventoryData, _inventoryDatabase);
        }

        private void AddEvents()
        {
            _presenter.OnItemEquipEvent += OnItemEquipped;
        }
        private void RemoveEvents()
        {
            _presenter.OnItemEquipEvent -= OnItemEquipped;
        }
    }
}