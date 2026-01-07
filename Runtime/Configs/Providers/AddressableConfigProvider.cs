using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLib.Configs.Models;
using CoreLib.Configs.Providers.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CoreLib.Configs.Providers
{
    public class AddressableConfigProvider : IConfigProvider
    {
        private readonly Dictionary<Type, ScriptableObject> _cache = new();

        public bool CanProvide(Type type)
        {
            // Мы не можем заранее проверить наличие ключа в Addressables без запроса.
            // Поэтому по умолчанию возвращаем true, и просто пробуем загрузить.
            return typeof(ScriptableObject).IsAssignableFrom(type);
        }

        public async Task<ScriptableObject> LoadConfigAsync(Type type)
        {
            var t = Resources.Load<KeyConfig>("Configs/KeyConfig");
            var entry = t.entries.FirstOrDefault(e => e.typeName == type.Name);
            if (entry?.configReference == null)
            {
                Debug.LogWarning($"No Addressable config reference found for {type.Name}");
                return null;
            }

            var key = entry.configReference;
            AsyncOperationHandle<ScriptableObject> handle;

            try
            {
                handle = Addressables.LoadAssetAsync<ScriptableObject>(key);
                await handle.Task;

                if (handle.Status == AsyncOperationStatus.Succeeded) return handle.Result;

                Debug.LogWarning($"[Addressables] Failed to load {key}, status: {handle.Status}");
                return null;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[Addressables] Exception while loading {key}: {ex.Message}");
                return null;
            }
        }
    }
}