using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets
{
    public class AddressablesAssetProvider : IAssetProvider
    {
        public async Task<T> LoadAsync<T>(string key, IProgress<float> progress = null) where T : UnityEngine.Object
        {
            var handle = Addressables.LoadAssetAsync<T>(key);

            while (!handle.IsDone)
            {
                progress?.Report(handle.PercentComplete);
                await Task.Yield();
            }

            if (handle.Status == AsyncOperationStatus.Succeeded)
                return handle.Result;

            Debug.LogError($"[AddressablesAssetProvider] Failed to load: {key}");
            return null;
        }

        public void Release(UnityEngine.Object asset)
        {
            Addressables.Release(asset);
        }
    }
}