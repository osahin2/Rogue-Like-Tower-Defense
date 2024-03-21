using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.UI.Inventory
{
    public class ItemEquipSlot : MonoBehaviour
    {
        [SerializeField] private InventoryItemType _type;
        [SerializeField] private Image _itemImage;

        public InventoryItemType Type => _type;

        public void SetItem(InventoryItemConfig config)
        {
            _itemImage.sprite = config.SpriteSettings.Sprite;
            _itemImage.rectTransform.sizeDelta = 
                new Vector2(config.SpriteSettings.width, config.SpriteSettings.height);
        }
    }
}