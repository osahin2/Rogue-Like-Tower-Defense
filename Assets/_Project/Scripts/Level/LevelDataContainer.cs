using Rogue_Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Rogue_LevelData
{
    [CreateAssetMenu(fileName = "LevelData", menuName ="Level/Level Data Container")]
    public class LevelDataContainer : ScriptableObject
    {
        [SerializeField] private MapType _mapType;
        [SerializeField] private List<LevelDataSO> _levelDatas = new();
        [SerializeField] private float _waveDuration;

        private readonly Dictionary<int, LevelData> _levelsMap = new();

        public void Construct()
        {
            SetLevelDataDictionary();
        }
        public void GetLevelData(int level, out LevelData levelData)
        {
            if (!_levelsMap.TryGetValue(level, out levelData))
            {
                throw new KeyNotFoundException($"LevelDataContainer: {level}. LevelData is not found in Dictionary ");
            }
        }
        private void SetLevelDataDictionary()
        {
            for (int i = 0; i < _levelDatas.Count; i++)
            {
                var data = _levelDatas[i].Data;
                data.Level = i + 1;
                data.WaveDuration = _waveDuration;
                
                _levelsMap.Add(data.Level, data);
            }
        }

    }
    public enum MapType
    {
        Default
    }
}