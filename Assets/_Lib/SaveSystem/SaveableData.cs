using Data;
using System;
using UnityEngine;

namespace SaveLoad
{
    [Serializable]
    public class SaveableData<TData> where TData : ISaveData
    {
        [field: SerializeField] public string Id { get; set; } = Guid.NewGuid().ToString();

        [SerializeField] private TData _data;
        public void Bind(TData data)
        {
            _data = data;
            _data.Id = Id;
        }
        public TData GetData() => _data;
    }
}