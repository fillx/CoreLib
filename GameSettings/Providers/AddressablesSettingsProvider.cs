using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameConfig.Providers
{
    
#if ADDRESSABLES_ENABLED
    public class AddressablesSettingsProvider : ISettingsProvider
    {
        private readonly Dictionary<Type, ScriptableObject> _cache = new();
        
        public async Task<T> GetSettingsAsync<T>() where T : ScriptableObject
        {
            var type = typeof(T);

            if (_cache.TryGetValue(type, out var cached))
                return cached as T;

            string key = type.Name;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _cache[type] = handle.Result as ScriptableObject;
                return handle.Result;
            }
            else
            {
                Debug.LogError($"Failed to load Addressable setting: {key}");
                return null;
            }
        }
    }
#endif  
}