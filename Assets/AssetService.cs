using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pool;
using UnityEngine;

namespace Assets
{
    public class AssetService : IAssetService
    {
        private readonly IAssetProvider _provider;
        private readonly Dictionary<string, UnityEngine.Object> _loadedAssets = new();

        public AssetService(IAssetProvider provider)
        {
            _provider = provider;
        }

        public async Task<AssetHandle<T>> LoadAsync<T>(string key, IProgress<float> progress = null)
            where T : UnityEngine.Object
        {
            if (_loadedAssets.TryGetValue(key, out var cached))
                return new AssetHandle<T>(key, (T)cached, this);

            
            T asset = await _provider.LoadAsync<T>(key, progress);
            _loadedAssets[key] = asset;
            return new AssetHandle<T>(key, asset, this);
        }

        public async Task PreloadAsync<T>(string key, IProgress<float> progress = null) where T : UnityEngine.Object
        {
            if (_loadedAssets.ContainsKey(key)) return;
            T asset = await _provider.LoadAsync<T>(key, progress);
            _loadedAssets[key] = asset;
        }

        public async Task<AssetInstanceHandle<T>> InstantiateAsync<T>(string key, IProgress<float> progress = null)
            where T : UnityEngine.Object
        {
            var handle = await LoadAsync<T>(key, progress);
            var instance = UnityEngine.Object.Instantiate(handle.Asset);

            return new AssetInstanceHandle<T>(
                key,
                instance,
                inst => UnityEngine.Object.Destroy(inst)
            );
        }

        public async Task<PoolableAssetInstanceHandle<T>> InstantiateFromPoolAsync<T>(string key, IPool<T> pool,
            IProgress<float> progress = null) where T : UnityEngine.Object
        {
            var handle = await LoadAsync<T>(key, progress);
            T instance = pool.Spawn(handle.Asset);
            return new PoolableAssetInstanceHandle<T>(key, instance, pool);
        }

        public void Release<T>(T asset) where T : UnityEngine.Object
        {
            if (asset == null) return;

            var key = _loadedAssets.FirstOrDefault(kvp => kvp.Value == asset).Key;
            if (!string.IsNullOrEmpty(key))
            {
                _loadedAssets.Remove(key);
                _provider.Release(asset);
            }
        }

        public void ClearCache(bool releaseAssets = true)
        {
            if (releaseAssets)
            {
                foreach (var asset in _loadedAssets.Values)
                    _provider.Release(asset);
            }

            _loadedAssets.Clear();
        }

        public async Task UnloadUnusedAsync()
        {
            await Resources.UnloadUnusedAssets();
            GC.Collect();
        }
    }
}