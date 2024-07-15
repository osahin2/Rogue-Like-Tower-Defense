using System.Collections.Generic;
using UnityEngine;

public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> _keys = new();
    [SerializeField] private List<TValue> _values = new();
    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();
        foreach (var pair in this)
        {
            _keys.Add(pair.Key);
            _values.Add(pair.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        Clear();
        if (_keys.Count != _values.Count)
        {
            Debug.LogError($"SerializableDictionary: The amount of keys {_keys.Count} does not match the amount of " +
                $"values{_values.Count}");

        }
        for (int i = 0; i < _keys.Count; i++)
        {
            Add(_keys[i], _values[i]);
        }
    }

}