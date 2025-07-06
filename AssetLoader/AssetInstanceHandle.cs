using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class AssetInstanceHandle<T> : IDisposable where T : UnityEngine.Object
    {
        public T Instance { get; }
        private readonly Action<T> _onDispose;

        public AssetInstanceHandle(string key, T instance, Action<T> onDispose)
        {
            Instance = instance;
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            _onDispose?.Invoke(Instance);
        }

        public static implicit operator T(AssetInstanceHandle<T> handle) => handle.Instance;
    }
}