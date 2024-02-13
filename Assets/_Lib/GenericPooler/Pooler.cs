using System;
using System.Collections.Generic;

namespace Pool
{
    [Serializable]
    public class PoolItem<T>
    {
        public T poolObject;
        public int initialAmountPool;
    }

    public interface IPooler<T> where T : class
    {
        T GetPooled();
        void Free(T obj);

        void Clear();
    }
    public class Pooler<T> : IPooler<T> where T : class
    {
        private Queue<T> _pooledObjects = new();
        private HashSet<T> _activatedObjects = new();

        private Func<T> _createFunc;
        private Action<T> _onFreeAction;
        private Action<T> _onGetAction;
        private Action<T> _onDestroyAction;
        private Action<T> _onInitialSpawnAction;
        private PoolItem<T> _poolItem;

        public Pooler(PoolItem<T> poolItem, Func<T> createFunc, Action<T> onGet, Action<T> onFree, Action<T> onDestroy, Action<T> onInitialSpawn)
        {
            _poolItem = poolItem;
            _createFunc = createFunc;
            _onGetAction = onGet;
            _onDestroyAction = onDestroy;
            _onFreeAction = onFree;
            _onInitialSpawnAction = onInitialSpawn;

            if (poolItem.initialAmountPool == 0) return;

            for (int i = 0; i < _poolItem.initialAmountPool; i++)
            {
                var obj = _createFunc();
                _pooledObjects.Enqueue(obj);
                _onInitialSpawnAction?.Invoke(obj);
            }

        }

        public T GetPooled()
        {
            T obj;
            if (_pooledObjects.Count == 0)
            {
                obj = _createFunc();
                _activatedObjects.Add(obj);
            }
            else
            {
                obj = _pooledObjects.Dequeue();
                _activatedObjects.Add(obj);
            }
            _onGetAction?.Invoke(obj);
            return obj;
        }
        public void Free(T obj)
        {
            if (!_activatedObjects.Contains(obj)) return;

            _pooledObjects.Enqueue(obj);
            _activatedObjects.Remove(obj);

            _onFreeAction?.Invoke(obj);
        }

        public void Clear()
        {
            foreach (var item in _pooledObjects)
            {
                _onDestroyAction?.Invoke(item);
            }
            foreach (var item in _activatedObjects)
            {
                _onDestroyAction?.Invoke(item);
            }

            _pooledObjects.Clear();
            _activatedObjects.Clear();
        }
    }
}