using Rogue_Enemy;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Rogue_LevelData
{
    [CreateAssetMenu(fileName ="LevelData", menuName ="Level/Level Data")]
    public class LevelDataSO : ScriptableObject
    {
        [SerializeField] private LevelData _levelData;
        public LevelData Data => _levelData;
    }

    [Serializable]
    public struct LevelData
    {
        [HideInInspector] public int Level;
        [SerializeField] private Sprite _levelIcon;
        [SerializeField] private string _levelName;

        public readonly Sprite Icon => _levelIcon;
        public readonly string Name => _levelName;

        public List<WaveData> Waves;
        public float WaveDuration { get; set; }
    }
    [Serializable]
    public struct WaveData
    {
        public List<WaveEnemyData> WaveEnemies;
    }
    [Serializable]
    public struct WaveEnemyData
    {
        [SerializeField] private string _name;
        public EnemyType EnemyType;
        public int TotalCount;
        public int SpawnedEnemy { get; set; }
        public float SpawnInterval { get; set; }

    }
}