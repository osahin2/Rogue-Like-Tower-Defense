using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pool
{
    [Serializable]
    public class ListPoolItem<T>
    {
        public string sortingTag;
        public T objectToPool;
        public int amountToPool;
        public bool autoExpand;
    }
    public class ListPooler<T> where T : MonoBehaviour
    {
        private Dictionary<string, Queue<T>> _pooledObjects;
        private Dictionary<string, HashSet<GameObject>> _activatedObjects = new();
        private Dictionary<string, ListPoolItem<T>> _lookupDictionary = new();
        private List<ListPoolItem<T>> _objectToPoolList;
        private Transform _parentObject;

        public ListPooler(List<ListPoolItem<T>> original, Transform parent)
        {
            _objectToPoolList = original;
            _parentObject = parent;
            _pooledObjects = new Dictionary<string, Queue<T>>();

            for (var i = 0; i < _objectToPoolList.Count; i++)
            {
                var poolItem = _objectToPoolList[i];

                _pooledObjects.Add(poolItem.sortingTag, new Queue<T>());
                _activatedObjects.Add(poolItem.sortingTag, new HashSet<GameObject>());
                _lookupDictionary.Add(poolItem.sortingTag, poolItem);

                for (int k = 0; k < poolItem.amountToPool; k++)
                {
                    var obj = Object.Instantiate(poolItem.objectToPool, parent);
                    obj.transform.SetParent(parent);
                    obj.gameObject.SetActive(false);
                    obj.name = poolItem.sortingTag;
                    _pooledObjects[poolItem.sortingTag].Enqueue(obj);
                }
            }
        }

        public T GetPooled(string tag)
        {
            T obj;

            if (_pooledObjects[tag].Count > 0)
            {
                obj = GetPooledFromDict(tag);
            }
            else if (_lookupDictionary[tag].autoExpand)
            {
                var poolItem = _objectToPoolList.Find(x => x.sortingTag == tag).objectToPool;
                obj = Object.Instantiate(poolItem, _parentObject);
                obj.name = tag;
                _activatedObjects[tag].Add(obj.gameObject);
            }
            else
            {
                obj = null;
            }
            return obj;
        }

        private T GetPooledFromDict(string tag)
        {
            T targetObj;
            if (_pooledObjects.ContainsKey(tag))
            {
                targetObj = _pooledObjects[tag].Dequeue();
                _activatedObjects[tag].Add(targetObj.gameObject);
            }
            else
            {
                Debug.LogError("Tag is not found");
                return null;
            }
            return targetObj;
        }

        public void Free(T obj)
        {
            var tag = obj.gameObject.name;
            if (!_activatedObjects.ContainsKey(tag)) return;
            if (!_activatedObjects[tag].Contains(obj.gameObject)) return;

            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_parentObject);
            _activatedObjects[tag].Remove(obj.gameObject);
            _pooledObjects[tag].Enqueue(obj);
        }
    }
}