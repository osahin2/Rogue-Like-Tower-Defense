using Rogue_LevelData;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UIScripts.Level
{
    public class LevelInfoUI : MonoBehaviour
    {
        public event Action<int> OnSelected;

        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _levelName;
        [SerializeField] private Button _selectButton;
        [SerializeField] private GameObject _selectedButton;

        public int Level => _levelData.Level;

        private LevelData _levelData;
        public void Construct(LevelData data)
        {
            _levelData = data;
            _icon.sprite = data.Icon;
            _levelName.text = data.Name;
        }
        public void Init()
        {
            _selectButton.onClick.AddListener(() =>
            {
                OnSelected?.Invoke(_levelData.Level);
            });
        }
        public void Select()
        {
            _selectButton.gameObject.SetActive(false);
            _selectedButton.SetActive(true);
        }
        public void DeSelect()
        {
            _selectButton.gameObject.SetActive(true);
            _selectedButton.SetActive(false);
        }
        public void DeInit()
        {
            _selectButton.onClick.RemoveAllListeners();
        }
    }
}