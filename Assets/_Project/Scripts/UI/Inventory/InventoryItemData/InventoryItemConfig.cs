using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.UI.Inventory
{
    public abstract class InventoryItemConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; set; } = Guid.NewGuid().ToString();
        public InventoryItemType ItemType;
        [SerializeField] protected SpriteSettings _spriteSettings;

        public int ConfigTypeAsEnum { get; protected set; }
        [field: NonSerialized] public List<ItemProperty> Stats { get; protected set; } = new();
        [field: NonSerialized] public bool IsEquipped { get; set; }
        public SpriteSettings SpriteSettings => _spriteSettings;
        public string ItemName { get; protected set; }
        
        public abstract void Construct();

        public InventoryItemData CreateSaveData()
        {
            return new InventoryItemData(this);
        }
    }
    [Serializable]
    public enum PropertyType
    {
        Damage,
        FireRate,
        Range
    }
    [Serializable]
    public class ItemProperty
    {
        public PropertyType Type;
        public Sprite Icon;
        public string Value { get; set; }

    }
    [Serializable]
    public struct SpriteSettings
    {
        public Sprite Sprite;
        public float height;
        public float width;
    }
}