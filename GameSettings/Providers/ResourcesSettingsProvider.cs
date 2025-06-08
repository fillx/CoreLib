using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameConfig.Providers
{
    public class ResourcesSettingsProvider : ISettingsProvider
    {
        private readonly Dictionary<Type, ScriptableObject> _cache = new();
        
        public Task<T> GetSettingsAsync<T>() where T : ScriptableObject
        {
            var type = typeof(T);
            if (_cache.TryGetValue(type, out var cached))
                return Task.FromResult(cached as T);

            var loaded = Resources.Load<T>(type.Name);
            _cache[type] = loaded as ScriptableObject;
            return Task.FromResult(loaded);
        }
    }
}