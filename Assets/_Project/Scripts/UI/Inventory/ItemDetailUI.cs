using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.UI.Inventory
{
    public class ItemDetailUI : MonoBehaviour
    {
        public event Action OnCloseClickEvent;
        public event Action OnItemEquipEvent;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _itemEquipButton;
        [SerializeField] private List<ItemPropertyUI> _itemPropertyPool = new();
        [SerializeField] private Transform _propertyItemParent;
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _itemNameText;

        public void Show(InventoryItemConfig config)
        {
            SetItemDetails(config);

            gameObject.SetActive(true);

            AddEvents();
        }
        public void Hide()
        {
            RemoveEvents();
            CloseProperties();
            gameObject.SetActive(false);
        }

        private void SetItemDetails(InventoryItemConfig config)
        {
            SetImage();
            SetProperties();
            SetItemName();

            return;

            void SetItemName()
            {
                _itemNameText.text = config.ItemName;
            }
            void SetProperties()
            {
                for (int i = 0; i < config.Stats.Count; i++)
                {
                    var item = config.Stats[i];
                    var propertyUI = _itemPropertyPool[i];
                    propertyUI.Init(item);
                }
            }
            void SetImage()
            {
                _itemImage.sprite = config.SpriteSettings.Sprite;
                _itemImage.rectTransform.sizeDelta =
                    new Vector2(config.SpriteSettings.width, config.SpriteSettings.height);
            }
        }
        private void CloseProperties()
        {
            foreach (var property in _itemPropertyPool)
            {
                property.DeInit();
            }
        }
        private void AddEvents()
        {
            _closeButton.onClick.AddListener(() =>
            {
                OnCloseClickEvent?.Invoke();
            });

            _itemEquipButton.onClick.AddListener(() =>
            {
                OnItemEquipEvent?.Invoke();
            });
        }
        private void RemoveEvents()
        {
            _closeButton.onClick.RemoveAllListeners();
            _itemEquipButton.onClick.RemoveAllListeners();
        }

    }
}