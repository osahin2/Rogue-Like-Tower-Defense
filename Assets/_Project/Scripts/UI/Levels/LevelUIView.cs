using System.Collections.Generic;
using UnityEngine;
namespace UIScripts.Level
{
    public class LevelUIView : SView
    {
        [SerializeField] private Transform _infoUIParent;
        [SerializeField] private LevelInfoUI _infoUIPrefab;

        public List<LevelInfoUI> LevelInfos { get; private set; }

        private readonly Dictionary<int, LevelInfoUI> _levelInfoDict = new();

        protected override void OnInit()
        {
            SetLevelInfoDictionary();
        }
        protected override void OnDeInit()
        {

        }
        protected override void OnShow()
        {
            gameObject.SetActive(true);
        }
        protected override void OnHide()
        {
            gameObject.SetActive(false);
        }
        private void SetLevelInfoDictionary()
        {
            foreach (var info in LevelInfos)
            {
                _levelInfoDict.Add(info.Level, info);
            }
        }
        public void SelectInfoUI(int level)
        {
            if (_levelInfoDict.TryGetValue(level, out var info))
            {
                info.Select();
            }
        }
        public void DeSelectInfoUI(int level)
        {
            if (_levelInfoDict.TryGetValue(level, out var info))
            {
                info.DeSelect();
            }
        }
        public void CreateLevelInfoUI()
        {
            var infoUi = Instantiate(_infoUIPrefab, _infoUIParent);
            LevelInfos.Add(infoUi);
        }
        protected override void OnAddEvents()
        {

        }
        protected override void OnRemoveEvents()
        {

        }

    }
}