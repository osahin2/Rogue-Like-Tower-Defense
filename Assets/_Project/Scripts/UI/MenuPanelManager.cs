using EventBusSystem;
using MenuEvent;
using System.Collections.Generic;
using UIScripts.Menu;
using UnityEngine;

namespace UIScripts
{
    public class MenuPanelManager : MonoBehaviour
    {
        [SerializeField] private MainMenu _menu;
        [SerializeField] private List<MenuPanel> _menuPanels = new();

        private readonly Dictionary<MenuState, MenuPanel> _menuPanelDict = new();

        private MenuPanel _currentPanel;
        public void Construct()
        {
            _menu.Construct();
            ConstructPanels();
            SetMenuPanelDict();
        }
        public void Show()
        {
            AddEvents();
            _menu.Show();
        }
        public void Hide()
        {
            RemoveEvents();
            _menu.Hide();
            _currentPanel.Hide();
        }
        private void SetMenuPanelDict()
        {
            foreach (var panel in _menuPanels)
            {
                _menuPanelDict.Add(panel.MenuState, panel);
            }
        }
        private void ConstructPanels()
        {
            foreach (var panel in _menuPanels)
            {
                panel.Construct();
            }
        }
        private void MenuButtonClicked(MenuEvents.MenuButton args)
        {
            if (_currentPanel != null)
            {
                _currentPanel.Hide();
            }
            if (_menuPanelDict.TryGetValue(args.State, out var panel))
            {
                _currentPanel = panel;
                _currentPanel.Show();
            }
        }

        private void AddEvents()
        {
            EventBus<MenuEvents.MenuButton>.RegisterEvent(MenuEvents.MENU_BUTTON_CLICK, MenuButtonClicked);
        }
        private void RemoveEvents()
        {
            EventBus<MenuEvents.MenuButton>.DeRegisterEvent(MenuEvents.MENU_BUTTON_CLICK, MenuButtonClicked);
        }
    }
}