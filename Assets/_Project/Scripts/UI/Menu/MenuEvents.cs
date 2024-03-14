using Rogue_LevelData;
using System;
using UIScripts.Menu;

namespace MenuEvent
{
    public class MenuEvents
    {
        public const string MENU_BUTTON_CLICK = "MenuButtonClicked";
        public const string BATTLE_BUTTON_CLICK = "BattleButtonClicked";
        public class MenuButton : EventArgs
        {
            public MenuState State { get; }
            public MenuButton(MenuState state)
            {
                State = state;
            }
        }

        public class BattleButton : EventArgs
        {

        }
    }
}