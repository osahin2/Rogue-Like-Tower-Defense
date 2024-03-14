using System;
using System.Collections.Generic;
using EventBusSystem;
using Lib;
using MenuEvent;

namespace UIScripts.Menu
{
    public class MenuModel : SModel
    {
        public List<MenuTabData> MenuTabs { get; private set; } = new();
        public Observable<MenuTabData> CurrentTab { get; private set; }

        private readonly Dictionary<MenuState, MenuTabData> _menuTabDict = new();
        protected override void OnInit()
        {
            CurrentTab = new Observable<MenuTabData>(null, OnTabValueChanged);
            SetMenuTabDictionary();
        }
        protected override void OnDeInit()
        {
            CurrentTab.Dispose();
        }
        private void SetMenuTabDictionary()
        {
            foreach (var tab in MenuTabs)
            {
                _menuTabDict.Add(tab.State, tab);
            }
        }
        public void SetMenuTabs(List<MenuTabData> tabs)
        {
            MenuTabs = tabs;
        }

        public void SetCurrentTab(MenuState state)
        {
            if (!_menuTabDict.TryGetValue(state, out var tab))
            {
                throw new KeyNotFoundException($"Menu Model: {state} not found in Menu Tab Dictionary");
            }
            
            CurrentTab.Value = tab;
        }

        private void OnTabValueChanged(MenuTabData data)
        {
            EventBus<MenuEvents.MenuButton>.TriggerEvent(MenuEvents.MENU_BUTTON_CLICK,
                new MenuEvents.MenuButton(data.State));
        }

    }
}