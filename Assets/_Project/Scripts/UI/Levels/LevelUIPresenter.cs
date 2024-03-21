using Data;
using Rogue_LevelData;
namespace UIScripts.Level
{
    public class LevelUIPresenter : SPresenter<LevelUIModel, LevelUIView>
    {
        private LevelUIPresenter(LevelUIModel model, LevelUIView view) : base(model, view)
        {
        }

        protected override void OnInit()
        {

        }
        protected override void OnDeInit()
        {

        }
        protected override void OnShow()
        {
            _view.SelectInfoUI(_model.CurrentSelectedData.Value.Level);
        }
        protected override void OnHide()
        {

        }
        public void Bind(LevelSaveData levelData)
        {
            _model.Bind(levelData);
        }
        private void InfoUIOnSelected(int level)
        {
            _view.DeSelectInfoUI(_model.CurrentSelectedData.Value.Level);
            _model.SetCurrentLevelData(level);
            _view.SelectInfoUI(level);
        }

        protected override void OnAddEvents()
        {
            foreach (var infoUI in _view.LevelInfos)
            {
                infoUI.OnSelected += InfoUIOnSelected;
            }
        }


        protected override void OnRemoveEvents()
        {
            foreach (var infoUI in _view.LevelInfos)
            {
                infoUI.OnSelected -= InfoUIOnSelected;
            }
        }
        public class Builder
        {
            private readonly LevelUIModel _model = new();

            public Builder WithLevelDatas(LevelDataContainer container)
            {
                container.Construct();
                _model.SetLevelDataContainer(container);
                return this;
            }
            public LevelUIPresenter Build(LevelUIView view)
            {
                for (int i = 0; i < _model.DataContainer.LevelDataCount; i++)
                {
                    view.CreateLevelInfoUI();
                }
                return new LevelUIPresenter(_model, view);
            }
        }
    }
}