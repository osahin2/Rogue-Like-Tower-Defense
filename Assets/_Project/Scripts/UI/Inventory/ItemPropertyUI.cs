using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.UI.Inventory
{
    public class ItemPropertyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _propertyValue;
        [SerializeField] private TextMeshProUGUI _propertyName;
        [SerializeField] private Image _propertyImage;

        private ItemProperty _property;

        public void Init(ItemProperty property)
        {
            _property = property;
            SetProperties();
            gameObject.SetActive(true);
        }
        public void DeInit()
        {
            _property = null;
            gameObject.SetActive(false);
        }
        private void SetProperties()
        {
            _propertyValue.text = _property.Value;
            _propertyName.text = _property.Type.ToString();
            _propertyImage.sprite = _property.Icon;
        }
    }
}