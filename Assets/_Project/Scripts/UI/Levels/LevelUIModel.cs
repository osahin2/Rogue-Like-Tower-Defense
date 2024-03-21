using Data;
using Lib;
using Rogue_LevelData;
namespace UIScripts.Level
{
    public class LevelUIModel : SModel
    {
        public LevelDataContainer DataContainer { get; private set; }
        public Observable<LevelData> CurrentSelectedData { get; private set; }

        private LevelSaveData _levelData;

        protected override void OnInit()
        {
            CurrentSelectedData = new Observable<LevelData>(_levelData.LevelData, OnLevelDataSelected);
        }
        protected override void OnDeInit()
        {
            CurrentSelectedData.Dispose();
        }
        public void Bind(LevelSaveData levelData)
        {
            _levelData = levelData;
        }
        public void SetLevelDataContainer(LevelDataContainer container)
        {
            DataContainer = container;
        }

        public void SetCurrentLevelData(int level)
        {
            DataContainer.GetLevelData(level, out var data);
            CurrentSelectedData.Value = data;
        }

        private void OnLevelDataSelected(LevelData levelData)
        {
            _levelData.LevelData = levelData;
        }
    }
}