using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class ResourcesAssetProvider : IAssetProvider
    {
        public async Task<T> LoadAsync<T>(string key, IProgress<float> progress = null) where T : UnityEngine.Object
        {
            // Resources.LoadAsync работает в фоне
            ResourceRequest request = Resources.LoadAsync<T>(key);
        
            while (!request.isDone)
            {
                progress?.Report(request.progress);
                await Task.Yield();
            }

            if (request.asset is T asset)
                return asset;

            Debug.LogError($"[ResourcesAssetProvider] Failed to load: {key}");
            return null;
        }

        public void Release(UnityEngine.Object asset)
        {
            // В Resources не обязательно делать Unload, но можно:
            // Если asset не используется — Resources.UnloadUnusedAssets выгрузит его
            // Resources.UnloadAsset(asset); // <-- если точно не нужен
        }
    }
}