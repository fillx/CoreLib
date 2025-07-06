using System;
using System.Threading.Tasks;
using Pool;

namespace Assets
{
    public interface IAssetService
    {
        Task<AssetHandle<T>> LoadAsync<T>(string key, IProgress<float> progress = null) where T : UnityEngine.Object;

        Task PreloadAsync<T>(string key, IProgress<float> progress = null) where T : UnityEngine.Object;

        Task<AssetInstanceHandle<T>> InstantiateAsync<T>(string key, IProgress<float> progress = null) where T : UnityEngine.Object;

        Task<PoolableAssetInstanceHandle<T>> InstantiateFromPoolAsync<T>(string key, IPool<T> pool, IProgress<float> progress = null) where T : UnityEngine.Object;

        void Release<T>(T asset) where T : UnityEngine.Object;

        void ClearCache(bool releaseAssets = true);

        Task UnloadUnusedAsync();
    }
}