using System.Collections.Generic;
using UnityEngine;

namespace UIScripts.Menu
{
    public class MenuView : SView
    {
        [SerializeField] private List<MenuTabButton> _menuButtons;
        [SerializeField] private RectTransform _menuTabsParents;

        public List<MenuTabButton> MenuButtons => _menuButtons;

        private readonly Dictionary<MenuState, MenuTabButton> _menuButtonsDict = new();

        protected override void OnInit()
        {
            InitMenuButtons();
            SetButtonDictionary();
        }
        protected override void OnDeInit()
        {
            DeInitMenuButtons();
        }
        private void SetButtonDictionary()
        {
            foreach (var button in _menuButtons)
            {
                _menuButtonsDict.Add(button.State, button);
            }
        }
        private void InitMenuButtons()
        {
            foreach (var tabButton in _menuButtons)
            {
                tabButton.Init();
            }
        }
        private void DeInitMenuButtons()
        {
            foreach (var tabButton in _menuButtons)
            {
                tabButton.DeInit();
            }
        }

        public void SetMenuButtons(List<MenuTabData> tabDatas)
        {
            foreach (var data in tabDatas)
            {
                var button = data.CreateButton(_menuTabsParents);
                _menuButtons.Add(button);
            }
        }
        public void SelectMenuTab(MenuState state)
        {
            if (_menuButtonsDict.TryGetValue(state, out var button))
            {
                button.Select();
            }
        }
        public void DeSelectMenuTab(MenuState state)
        {
            if (_menuButtonsDict.TryGetValue(state, out var button))
            {
                button.DeSelect();
            }
        }
        public void DeSelectAllTabs()
        {
            foreach (var tabButton in _menuButtons)
            {
                tabButton.DeSelect();
            }
        }
        protected override void OnShow()
        {
            gameObject.SetActive(true);
        }
        protected override void OnHide()
        {
            gameObject.SetActive(false);
        }
        protected override void OnAddEvents()
        {

        }
        protected override void OnRemoveEvents()
        {

        }

    }
}