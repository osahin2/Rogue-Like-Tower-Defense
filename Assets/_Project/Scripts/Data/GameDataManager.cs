using SaveLoad;
using Service_Locator;
using System;
using UnityEngine;

namespace Data
{
    public interface IGameDataManager
    {
        GameData GameData { get; }
        void Save();
        void Load();
    }
    public class GameDataManager : MonoBehaviour, IGameDataManager
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private InventoryDatabase _inventoryData;

        private ISaveSystem _saveSystem;

        public GameData GameData => _gameData;

        public void Construct()
        {
            _saveSystem = new SaveSystem(new JsonSerializer());
            Load();
            ConstructData();

            ServiceProvider.Instance.Register<IInventoryData>(_inventoryData);
        }
        private void ConstructData()
        {
            _inventoryData.Construct();
        }
        public void Save()
        {
            _saveSystem.Save(_gameData);
        }

        public void Load()
        {
            if (string.IsNullOrWhiteSpace(_gameData.name))
            {
                _gameData = new GameData { name = "data" };
            }
            if (_saveSystem.TryLoad<GameData>(_gameData.name, out var data))
            {
                _gameData = data;
            }

        }
    }

    [Serializable]
    public class GameData : GameDataBase
    {
        [SerializeField] private PlayerSaveData _playerData;
        [SerializeField] private InventorySaveData _inventorySaveData;
        [SerializeField] private LevelSaveData _levelSaveData;
        public PlayerSaveData PlayerData => _playerData ??= new PlayerSaveData();
        public InventorySaveData InventoryData => _inventorySaveData ??= new InventorySaveData();
        public LevelSaveData LevelData => _levelSaveData ??= new LevelSaveData();
    }
}