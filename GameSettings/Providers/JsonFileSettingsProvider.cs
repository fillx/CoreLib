using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace GameConfig.Providers
{
    public class JsonFileSettingsProvider : ISettingsProvider
    {
        private readonly string _path;
        private readonly Dictionary<string, object> _cache = new();

        public JsonFileSettingsProvider(string path)
        {
            _path = path;
        }
        
        public Task<T> GetSettingsAsync<T>() where T : ScriptableObject
        {
            
            var key = typeof(T).Name;

            if (_cache.TryGetValue(key, out var cached))
                return Task.FromResult(cached as T);

            var fullPath = Path.Combine(_path, $"{key}.json");

            if (!File.Exists(fullPath))
            {
                Debug.LogWarning($"[JsonFileSettingsProvider] File not found: {fullPath}");
                return Task.FromResult<T>(null);
            }

            var json = File.ReadAllText(fullPath);
            var obj = JsonUtility.FromJson<T>(json);
            _cache[key] = obj;
            return Task.FromResult(obj);
        }
    }
}