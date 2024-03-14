﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.UI.Inventory
{
    public class InventoryItemUI : MonoBehaviour
    {
        public event Action<InventoryItemConfig> OnItemClickEvent;

        [SerializeField] private InventoryItemType _itemType;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;

        private InventoryItemConfig _itemConfig;
        public InventoryItemType ItemType => _itemType;


        public void Init(InventoryItemConfig config)
        {
            _itemConfig = config;
            SetItemIcon();

            _button.onClick.AddListener(OnItemClicked);
        }
        private void SetItemIcon()
        {
            _icon.sprite = _itemConfig.Icon;
        }
        private void OnItemClicked()
        {
            OnItemClickEvent?.Invoke(_itemConfig);
        }
    }
}