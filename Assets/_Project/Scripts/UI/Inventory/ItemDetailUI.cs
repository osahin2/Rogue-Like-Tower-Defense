using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.UI.Inventory
{
    public class ItemDetailUI : MonoBehaviour
    {
        public event Action OnCloseClickEvent;
        public event Action OnItemEquipEvent;

        [SerializeField] private TextMeshProUGUI _itemDetailText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _itemEquipButton;

        public void Show(InventoryItemConfig config)
        {
            SetItemDetailText(config);

            gameObject.SetActive(true);

            AddEvents();
        }
        public void Hide()
        {
            RemoveEvents();

            gameObject.SetActive(false);
        }

        private void SetItemDetailText(InventoryItemConfig config)
        {
            var statText = "";
            if (config.Stats.Count > 0)
            {
                foreach (var stat in config.Stats)
                {
                    statText += stat.Key + stat.Value + "\n";
                }
            }
            var itemDetailText = $"<b>{config.name}<b>\n\n<b>{statText}</b>";
            _itemDetailText.text = itemDetailText;
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