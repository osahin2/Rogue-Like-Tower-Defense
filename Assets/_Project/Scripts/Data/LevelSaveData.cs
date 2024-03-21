using Rogue_LevelData;
using SaveLoad;
using System;
using UnityEngine;
namespace Data
{
    [Serializable]
    public class LevelSaveData : ISaveData
    {
        [field: SerializeField] public string Id { get; set; }
        public LevelData LevelData;
    }
}
