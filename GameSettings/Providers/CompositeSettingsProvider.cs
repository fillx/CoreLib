using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSettings.Providers
{
    public class CompositeSettingsProvider : ISettingsProvider
    {
        private readonly List<ISettingsProvider> _providers;
        private readonly Dictionary<Type, object> _cache = new();
        
        public CompositeSettingsProvider(params ISettingsProvider[] providers)
        {
            _providers = new List<ISettingsProvider>(providers);
        }
        
        public async Task<T> GetSettingsAsync<T>() where T : ScriptableObject
        {
            if (_cache.TryGetValue(typeof(T), out var cached))
                return cached as T;

            foreach (var provider in _providers)
            {
                try
                {
                    var result = await provider.GetSettingsAsync<T>();
                    if (result != null)
                    {
                        _cache[typeof(T)] = result;
                        return result;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"SettingsProvider error for {typeof(T).Name}: {e.Message}");
                    continue;
                }
            }

            Debug.LogError($"[CompositeSettingsProvider] Could not load settings of type {typeof(T).Name}");
            return null;
        }
    }
}