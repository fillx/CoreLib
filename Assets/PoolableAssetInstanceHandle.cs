using System;
using Pool;

namespace Assets
{
    public class PoolableAssetInstanceHandle<T> : IDisposable where T : UnityEngine.Object
    {
        public string Key { get; }
        public T Instance { get; }
        private readonly IPool<T> _pool;

        public PoolableAssetInstanceHandle(string key, T instance, IPool<T> pool)
        {
            Key = key;
            Instance = instance;
            _pool = pool;
        }

        public void Dispose()
        {
            _pool?.Despawn(Instance);
        }
    }
}