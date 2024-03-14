using EventBusSystem;
using MenuEvent;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts.Play
{
    public class PlayView : MenuPanel
    {
        [SerializeField] private Button _battleButton;

        public override void Construct()
        {

        }
        public override void Show()
        {
            if (!_battleButton.enabled)
            {
                _battleButton.enabled = true;
            }

            gameObject.SetActive(true);
            AddEvents();
        }
        public override void Hide()
        {
            RemoveEvents();
            gameObject.SetActive(false);
        }

        private void OnBattleButtonClick()
        {
            EventBus<MenuEvents.BattleButton>.TriggerEvent(MenuEvents.BATTLE_BUTTON_CLICK,
                new MenuEvents.BattleButton());
            _battleButton.enabled = false;
        }
        private void AddEvents()
        {
            _battleButton.onClick.AddListener(OnBattleButtonClick);
        }
        private void RemoveEvents()
        {
            _battleButton.onClick.RemoveListener(OnBattleButtonClick);
        }

    }
}