using UnityEngine;

namespace UIScripts.Menu
{
    [CreateAssetMenu(fileName = "MenuTabData", menuName ="Main Menu/Menu Tab Data")]
    public class MenuTabData : ScriptableObject
    {
        [SerializeField] private MenuTabButton _buttonPrefab;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _tabName;
        [SerializeField] private MenuState _state;

        public Sprite Icon => _icon;
        public MenuState State => _state;
        public string TabName => _tabName;

        public MenuTabButton CreateButton(RectTransform parent)
        {
            var button = Instantiate(_buttonPrefab, parent);
            button.SetMenuTabData(this);
            return button;
        }
    }
}