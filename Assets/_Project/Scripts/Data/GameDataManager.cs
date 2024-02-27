using Player.Weapons;
using Service_Locator;
using System;
using UnityEngine;
using SaveLoad;
using Player;

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

        private ISaveSystem _saveSystem;

        public GameData GameData => _gameData;

        public void Awake()
        {
            _saveSystem = new SaveSystem(new JsonSerializer());
            Load();

            ServiceProvider.Instance.Register<IGameDataManager>(this);
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
                Debug.Log("Try Load Success");
                _gameData = data;
            }

        }
    }

    [Serializable]
    public class GameData : GameDataBase
    {
        [SerializeField] private PlayerData playerData;

        public PlayerData PlayerData
        {
            get
            {
                if (playerData == null)
                {
                    playerData = new PlayerData();
                }
                return playerData;
            }
        }
    }
}