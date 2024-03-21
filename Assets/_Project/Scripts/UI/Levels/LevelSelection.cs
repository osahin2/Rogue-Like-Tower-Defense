using Data;
using Rogue_LevelData;
using SaveLoad;
using Service_Locator;
using UnityEngine;

namespace UIScripts.Level
{
    public class LevelSelection : MenuPanel
    {
        [SerializeField] private LevelUIView _levelView;
        [SerializeField] private LevelDataContainer _levelDataContainer;
        [SerializeField] private SaveableData<LevelSaveData> _levelData;

        private LevelUIPresenter _levelPresenter;
        private IGameDataManager _gameDataManager;
        private LevelSaveData LevelData => _levelData.GetData();
        public override void Construct()
        {
            ServiceProvider.Instance.Get(out _gameDataManager);

            _levelPresenter = new LevelUIPresenter.Builder()
                .WithLevelDatas(_levelDataContainer)
                .Build(_levelView);

            Bind();
        }
        private void Bind()
        {
            _levelData.Bind(_gameDataManager.GameData.LevelData);
            _levelPresenter.Bind(LevelData);
        }
        public override void Show()
        {
            _levelPresenter.Init();
            _levelPresenter.Show();
        }

        public override void Hide()
        {
            _levelPresenter.Hide();
            _levelPresenter.DeInit();
        }

    }
}