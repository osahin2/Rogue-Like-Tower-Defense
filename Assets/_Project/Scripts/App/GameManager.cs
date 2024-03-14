using EventBusSystem;
using MenuEvent;
using Rogue.GameEvent;
using UIScripts;
using UnityEngine;

namespace App
{
    public enum GameState
    {
        Play,
        MainMenu,
        Pause
    }
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameContext _gameContext;

        private UIManager _uiManager;

        private void Awake()
        {
            Application.targetFrameRate = 60;

            _gameContext.Construct();
            _uiManager = _gameContext.UIManager;
        }
        private void Start()
        {
            _uiManager.OpenMenu();
        }

        private void OnBattleStart(MenuEvents.BattleButton args)
        {
            _uiManager.CloseMenu();
            _gameContext.EnemyManager.Init();
            _gameContext.PlayerController.Init();
        }

        private void OnBattleEnd(GameplayEvents.GameEnd args)
        {
            _uiManager.OpenMenu();
            _gameContext.EnemyManager.DeInit();
            _gameContext.PlayerController.DeInit();
        }

        private void OnEnable()
        {
            AddEvents();
        }
        private void OnDisable()
        {
            RemoveEvents();
        }

        private void OnApplicationPause(bool pause)
        {
            _gameContext.GameDataManager.Save();
        }
        private void OnApplicationQuit()
        {
            _gameContext.GameDataManager.Save();
        }

        private void AddEvents()
        {
            EventBus<MenuEvents.BattleButton>.RegisterEvent(MenuEvents.BATTLE_BUTTON_CLICK, OnBattleStart);
            EventBus<GameplayEvents.GameEnd>.RegisterEvent(GameplayEvents.GAME_END, OnBattleEnd);
        }
        private void RemoveEvents()
        {
            EventBus<MenuEvents.BattleButton>.DeRegisterEvent(MenuEvents.BATTLE_BUTTON_CLICK, OnBattleStart);
            EventBus<GameplayEvents.GameEnd>.DeRegisterEvent(GameplayEvents.GAME_END, OnBattleEnd);
        }
    }
}
